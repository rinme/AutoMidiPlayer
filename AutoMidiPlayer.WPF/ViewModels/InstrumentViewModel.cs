using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMidiPlayer.Data.Midi;
using AutoMidiPlayer.Data.Notification;
using AutoMidiPlayer.Data.Properties;
using AutoMidiPlayer.WPF.Core;
using AutoMidiPlayer.WPF.ModernWPF.Errors;
using JetBrains.Annotations;
using Melanchall.DryWetMidi.Multimedia;
using Stylet;
using StyletIoC;

namespace AutoMidiPlayer.WPF.ViewModels;

public class InstrumentViewModel : Screen
{
    private static readonly Settings Settings = Settings.Default;
    private readonly IContainer _ioc;
    private readonly IEventAggregator _events;
    private readonly MainWindowViewModel _main;
    private readonly OutputDevice? _speakers;
    private InputDevice? _inputDevice;

    public InstrumentViewModel(IContainer ioc, MainWindowViewModel main)
    {
        _ioc = ioc;
        _main = main;
        _events = ioc.Get<IEventAggregator>();

        // Initialize selected MIDI input
        SelectedMidiInput = MidiInputs[0];

        // Initialize instrument from settings
        SelectedInstrument = Keyboard.InstrumentNames
            .FirstOrDefault(i => (int)i.Key == Settings.SelectedInstrument);

        // Initialize layout from settings
        SelectedLayout = Keyboard.LayoutNames
            .FirstOrDefault(l => (int)l.Key == Settings.SelectedLayout);

        // Initialize UseDirectInput from settings
        UseDirectInput = Settings.UseDirectInput;
        LyrePlayer.UseDirectInput = UseDirectInput;

        try
        {
            _speakers = OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        }
        catch (ArgumentException e)
        {
            new ErrorContentDialog(e, closeText: "Ignore").ShowAsync();
            CanUseSpeakers = false;
            Settings.UseSpeakers = false;
        }
    }

    public BindableCollection<MidiInput> MidiInputs { get; } = new()
    {
        new("None")
    };

    public MidiInput? SelectedMidiInput { get; set; }

    public KeyValuePair<Keyboard.Instrument, string> SelectedInstrument { get; set; }

    public KeyValuePair<Keyboard.Layout, string> SelectedLayout { get; set; }

    public bool CanUseSpeakers { get; set; } = true;

    public bool UseDirectInput { get; set; }

    public bool CanChangeTime => PlayTimerToken is null;

    public bool CanStartStopTimer => DateTime - DateTime.Now > TimeSpan.Zero;

    [UsedImplicitly] public CancellationTokenSource? PlayTimerToken { get; private set; }

    public DateTime DateTime { get; set; } = DateTime.Now;

    public string TimerText => CanChangeTime ? "Start" : "Stop";

    private TrackViewModel TrackView => _main.TrackView;

    public void RefreshDevices()
    {
        MidiInputs.Clear();
        MidiInputs.Add(new("None"));

        foreach (var device in InputDevice.GetAll())
        {
            MidiInputs.Add(new(device.Name));
        }

        SelectedMidiInput = MidiInputs[0];
    }

    public void OnSelectedMidiInputChanged()
    {
        _inputDevice?.Dispose();

        if (SelectedMidiInput?.DeviceName is not null
            && SelectedMidiInput.DeviceName != "None")
        {
            _inputDevice = InputDevice.GetByName(SelectedMidiInput.DeviceName);

            _inputDevice!.EventReceived += OnNoteEvent;
            _inputDevice!.StartEventsListening();
        }
    }

    private void OnNoteEvent(object? sender, MidiEventReceivedEventArgs e)
    {
        if (e.Event is not Melanchall.DryWetMidi.Core.NoteOnEvent noteOn) return;
        if (noteOn.Velocity == 0) return;

        LyrePlayer.PlayNote(noteOn.NoteNumber, SelectedLayout.Key, SelectedInstrument.Key);
    }

    public void TestInstrument()
    {
        // Play a simple test note using the current instrument settings
        var testNotes = new[] { 60, 62, 64, 65, 67, 69, 71, 72 }; // C major scale

        Task.Run(async () =>
        {
            foreach (var note in testNotes)
            {
                if (Settings.UseSpeakers && _speakers is not null)
                {
                    // Play through speakers
                    var noteOnEvent = new Melanchall.DryWetMidi.Core.NoteOnEvent((Melanchall.DryWetMidi.Common.SevenBitNumber)note, (Melanchall.DryWetMidi.Common.SevenBitNumber)100);
                    _speakers.SendEvent(noteOnEvent);
                    await Task.Delay(150);
                    var noteOffEvent = new Melanchall.DryWetMidi.Core.NoteOffEvent((Melanchall.DryWetMidi.Common.SevenBitNumber)note, (Melanchall.DryWetMidi.Common.SevenBitNumber)0);
                    _speakers.SendEvent(noteOffEvent);
                }
                else
                {
                    // Play through keyboard input
                    LyrePlayer.PlayNote(note, SelectedLayout.Key, SelectedInstrument.Key);
                }
                await Task.Delay(100);
            }
        });
    }

    [UsedImplicitly]
    public async Task StartStopTimer()
    {
        if (PlayTimerToken is not null)
        {
            PlayTimerToken.Cancel();
            return;
        }

        PlayTimerToken = new();

        var start = DateTime - DateTime.Now;
        await Task.Delay(start, PlayTimerToken.Token)
            .ContinueWith(_ => { });

        if (!PlayTimerToken.IsCancellationRequested)
            _events.Publish(new PlayTimerNotification());

        PlayTimerToken = null;
    }

    [UsedImplicitly]
    public void SetTimeToNow() => DateTime = DateTime.Now;

    [UsedImplicitly]
    private void OnSelectedInstrumentChanged()
    {
        var instrument = (int)SelectedInstrument.Key;
        Settings.Modify(s => s.SelectedInstrument = instrument);
        _events.Publish(this);
    }

    [UsedImplicitly]
    private void OnSelectedLayoutChanged()
    {
        var layout = (int)SelectedLayout.Key;
        Settings.Modify(s => s.SelectedLayout = layout);
        _events.Publish(this);
    }

    [UsedImplicitly]
    private void OnUseDirectInputChanged()
    {
        Settings.UseDirectInput = UseDirectInput;
        Settings.Save();
        LyrePlayer.UseDirectInput = UseDirectInput;
    }
}
