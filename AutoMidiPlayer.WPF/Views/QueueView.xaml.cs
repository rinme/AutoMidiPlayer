using System.Windows;
using System.Windows.Controls;
using AutoMidiPlayer.WPF.ModernWPF;
using AutoMidiPlayer.WPF.ViewModels;

namespace AutoMidiPlayer.WPF.Views;

public partial class QueueView : UserControl
{
    private ListViewDragDropHelper? _dragDropHelper;

    public QueueView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is QueueViewModel viewModel && _dragDropHelper == null)
        {
            _dragDropHelper = new ListViewDragDropHelper(
                TrackList.ListView,
                viewModel.Tracks,
                viewModel.OnQueueModified);
        }
    }

    /// <summary>
    /// Handle double-click on a track - plays the song
    /// </summary>
    private void TrackList_ItemDoubleClick(object sender, RoutedEventArgs e)
    {
        if (e is TrackListEventArgs args && DataContext is QueueViewModel viewModel)
        {
            viewModel.PlayPauseFromQueue(args.File);
        }
    }

    /// <summary>
    /// Handle play/pause button click from TrackListControl
    /// </summary>
    private void TrackList_PlayPauseClick(object sender, RoutedEventArgs e)
    {
        if (e is TrackListEventArgs args && DataContext is QueueViewModel viewModel)
        {
            viewModel.PlayPauseFromQueue(args.File);
        }
    }

    /// <summary>
    /// Handle menu button click from TrackListControl
    /// </summary>
    private void TrackList_MenuClick(object sender, RoutedEventArgs e)
    {
        // Menu is automatically opened by TrackListControl
    }

    /// <summary>
    /// Remove selected song from queue
    /// </summary>
    private void RemoveFromQueue_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is QueueViewModel viewModel)
        {
            viewModel.RemoveTrack();
        }
    }

    /// <summary>
    /// Edit selected song
    /// </summary>
    private async void EditSong_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is QueueViewModel viewModel && viewModel.SelectedFile is not null)
        {
            await viewModel.EditSong(viewModel.SelectedFile);
        }
    }

    /// <summary>
    /// Delete selected song from library
    /// </summary>
    private async void DeleteSong_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is QueueViewModel viewModel && viewModel.SelectedFile is not null)
        {
            await viewModel.DeleteSong(viewModel.SelectedFile);
        }
    }
}
