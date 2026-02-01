using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMidiPlayer.Data;
using AutoMidiPlayer.Data.Properties;
using AutoMidiPlayer.WPF.Services;
using AutoMidiPlayer.WPF.Views;
using JetBrains.Annotations;
using ModernWpf;
using Stylet;
using StyletIoC;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using AutoSuggestBox = Wpf.Ui.Controls.AutoSuggestBox;
using MidiFile = AutoMidiPlayer.Data.Midi.MidiFile;

namespace AutoMidiPlayer.WPF.ViewModels;

[UsedImplicitly]
public class MainWindowViewModel : Conductor<IScreen>, IHandle<MidiFile>
{
    public static NavigationStore Navigation = null!;
    private readonly IThemeService _theme;
    private readonly IEventAggregator _events;
    private static readonly Settings Settings = Settings.Default;

    private static readonly string AppName = $"Auto MIDI Player {SettingsPageViewModel.ProgramVersion}";
    private static readonly string[] MidiExtensions = { ".mid", ".midi" };

    public MainWindowViewModel(IContainer ioc, IThemeService theme)
    {
        Title = AppName;

        Ioc = ioc;
        _theme = theme;
        _events = ioc.Get<IEventAggregator>();
        _events.Subscribe(this);

        // Initialize services FIRST - ViewModels depend on these
        // PlaybackService handles all playback logic
        Playback = new PlaybackService(ioc, this);

        // Initialize ViewModels - order matters for dependencies
        SettingsView = new(ioc, this);
        InstrumentView = new(ioc, this);

        // TrackView only handles track list management
        ActiveItem = TrackView = new(ioc, this);

        // QueueView and SongsView depend on Playback being initialized
        QueueView = new(ioc, this);
        SongsView = new(ioc, this);
        PianoSheetView = new(this);
    }

    public IContainer Ioc { get; }

    public PlaybackService Playback { get; }

    public void Handle(MidiFile message)
    {
        // Title will be updated when playback starts via UpdateTitle()
        UpdateTitle();
    }

    public void UpdateTitle()
    {
        // Only show song title when actively playing, not when paused or stopped
        if (Playback.IsPlaying && QueueView.OpenedFile is not null)
        {
            var title = QueueView.OpenedFile.Title;
            var author = QueueView.OpenedFile.Author;
            Title = string.IsNullOrWhiteSpace(author) ? title : $"{title} • {author}";
        }
        else
        {
            Title = AppName;
        }
    }

    public bool ShowUpdate => SettingsView.NeedsUpdate && ActiveItem != SettingsView;

    public SongsViewModel SongsView { get; }

    public TrackViewModel TrackView { get; }

    public PianoSheetViewModel PianoSheetView { get; }

    public QueueViewModel QueueView { get; }

    public SettingsPageViewModel SettingsView { get; }

    public InstrumentViewModel InstrumentView { get; }

    public string Title { get; set; }

    public void Navigate(INavigation sender, RoutedNavigationEventArgs args)
    {
        if ((args.CurrentPage as NavigationItem)?.Tag is IScreen viewModel)
        {
            ActivateItem(viewModel);

            // Save the page name for restoration on next launch
            var pageName = (args.CurrentPage as NavigationItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(pageName))
            {
                Settings.LastViewedPage = pageName;
                Settings.Save();
            }
        }

        NotifyOfPropertyChange(() => ShowUpdate);
    }

    public void NavigateToSettings() => ActivateItem(SettingsView);

    public void ToggleTheme()
    {
        ThemeManager.Current.ApplicationTheme = _theme.GetTheme() switch
        {
            ThemeType.Unknown => ApplicationTheme.Dark,
            ThemeType.Dark => ApplicationTheme.Light,
            ThemeType.Light => ApplicationTheme.Dark,
            ThemeType.HighContrast => ApplicationTheme.Dark,
            _ => ApplicationTheme.Dark
        };

        SettingsView.OnThemeChanged();
    }

    public void SearchSong(AutoSuggestBox sender, TextChangedEventArgs e)
    {
        if (ActiveItem != QueueView)
        {
            ActivateItem(QueueView);

            var queue = Navigation.Items
                .OfType<NavigationItem>()
                .First(nav => nav.Tag == QueueView);
            var index = Navigation.Items.IndexOf(queue);
            Navigation.SelectedPageIndex = index;
        }

        QueueView.FilterText = sender.Text;
    }

    public void OnDragOver(DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
            var hasMidiFiles = files.Any(f => MidiExtensions.Contains(
                System.IO.Path.GetExtension(f).ToLowerInvariant()));

            e.Effects = hasMidiFiles ? DragDropEffects.Copy : DragDropEffects.None;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
        e.Handled = true;
    }

    public async void OnFileDrop(DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

        var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
        var midiFiles = files.Where(f => MidiExtensions.Contains(
            System.IO.Path.GetExtension(f).ToLowerInvariant())).ToArray();

        if (midiFiles.Length > 0)
        {
            await SongsView.AddFiles(midiFiles);

            // Navigate to songs view
            ActivateItem(SongsView);
            var songs = Navigation.Items
                .OfType<NavigationItem>()
                .First(nav => nav.Tag == SongsView);
            var index = Navigation.Items.IndexOf(songs);
            Navigation.SelectedPageIndex = index;
        }
    }

    protected override async void OnViewLoaded()
    {
        Navigation = ((MainWindowView)View).RootNavigation;
        SettingsView.OnThemeChanged();

        // Restore last viewed page (default to Songs if not set)
        var lastPage = Settings.LastViewedPage;
        if (string.IsNullOrEmpty(lastPage)) lastPage = "Songs";

        var targetNavItem = Navigation.Items
            .OfType<NavigationItem>()
            .FirstOrDefault(nav => nav.Content?.ToString() == lastPage);

        if (targetNavItem != null)
        {
            var index = Navigation.Items.IndexOf(targetNavItem);
            Navigation.SelectedPageIndex = index;
            if (targetNavItem.Tag is IScreen viewModel)
                ActivateItem(viewModel);
        }

        if (!await SettingsView.TryGetLocationAsync()) _ = SettingsView.LocationMissing();
        if (SettingsView.AutoCheckUpdates)
        {
            _ = SettingsView.CheckForUpdate()
                .ContinueWith(_ => { NotifyOfPropertyChange(() => ShowUpdate); });
        }

        // Load songs from database into Songs library
        await using var db = Ioc.Get<LyreContext>();
        await SongsView.AddFiles(db.Songs);

        // Auto-scan MIDI folder if configured
        if (!string.IsNullOrEmpty(SettingsView.MidiFolder))
        {
            await SongsView.ScanFolder(SettingsView.MidiFolder);
        }

        // Restore queue from saved state
        QueueView.RestoreQueue(SongsView.Tracks);

        // Restore previously playing song and position
        var savedPosition = QueueView.RestoreCurrentSong(SongsView.Tracks);
        if (savedPosition.HasValue)
        {
            Playback.SetSavedPosition(savedPosition.Value);
        }
    }
}
