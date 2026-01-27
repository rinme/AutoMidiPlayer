using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Stylet;

namespace AutoMidiPlayer.Data.Midi;

public class MidiTrack : INotifyPropertyChanged
{
    private readonly IEventAggregator _events;
    private bool _isChecked;
    private bool _isActive;
    private Timer? _glowTimer;
    private HashSet<int>? _noteNumbers; // Cached note numbers for fast lookup
    private const int GlowDurationMs = 150; // How long the glow stays on

    // Black keys (sharps/flats) in MIDI note numbers mod 12: C#, D#, F#, G#, A#
    private static readonly HashSet<int> BlackKeys = new() { 1, 3, 6, 8, 10 };

    public MidiTrack(IEventAggregator events, TrackChunk track, int index, bool isChecked = true)
    {
        _events = events;
        _isChecked = isChecked;

        Track = track;
        Index = index;
        TrackName = track.Events.OfType<SequenceTrackNameEvent>().FirstOrDefault()?.Text;

        // Calculate statistics
        CalculateStatistics();
    }

    private void CalculateStatistics()
    {
        var notes = Track.GetNotes().ToList();
        NotesCount = notes.Count;

        // Cache note numbers for fast lookup during playback
        _noteNumbers = notes.Select(n => (int)n.NoteNumber).ToHashSet();

        if (NotesCount == 0)
        {
            BlackKeyRatio = 0;
            AverageDurationMs = 0;
            FrequentNotesRatio = 0;
            return;
        }

        // Black key ratio
        var blackKeyCount = notes.Count(n => BlackKeys.Contains(n.NoteNumber % 12));
        BlackKeyRatio = (double)blackKeyCount / NotesCount * 100;

        // Average duration in milliseconds
        var totalDuration = notes.Sum(n => n.LengthAs<MetricTimeSpan>(TempoMap.Default).TotalMicroseconds / 1000.0);
        AverageDurationMs = totalDuration / NotesCount;

        // Frequent notes ratio - notes that appear more than average
        var noteGroups = notes.GroupBy(n => n.NoteNumber).ToList();
        var averageOccurrence = (double)NotesCount / noteGroups.Count;
        var frequentNotes = noteGroups.Where(g => g.Count() > averageOccurrence).Sum(g => g.Count());
        FrequentNotesRatio = (double)frequentNotes / NotesCount * 100;
    }

    public bool CanBePlayed => Track.Events.Count(e => e is NoteEvent) > 0;

    public int Index { get; }

    public int NotesCount { get; private set; }

    public double BlackKeyRatio { get; private set; }

    public double AverageDurationMs { get; private set; }

    public double FrequentNotesRatio { get; private set; }

    // Formatted display strings
    public string NotesCountDisplay => $"{NotesCount:N0}";
    public string BlackKeyRatioDisplay => $"{BlackKeyRatio:F1}%";
    public string AverageDurationDisplay => $"{AverageDurationMs:F0}ms";
    public string FrequentNotesRatioDisplay => $"{FrequentNotesRatio:F1}%";

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            _events.Publish(this);
        }
    }

    public string? TrackName { get; }

    public TrackChunk Track { get; }

    /// <summary>
    /// Gets or sets whether this track is currently playing a note (for glow effect)
    /// </summary>
    public bool IsActive
    {
        get => _isActive;
        private set
        {
            if (_isActive != value)
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Triggers the glow effect for this track
    /// </summary>
    public void TriggerGlow()
    {
        // Stop existing timer if any
        _glowTimer?.Stop();
        _glowTimer?.Dispose();

        // Set active
        IsActive = true;

        // Create timer to turn off glow
        _glowTimer = new Timer(GlowDurationMs);
        _glowTimer.AutoReset = false;
        _glowTimer.Elapsed += (_, _) =>
        {
            IsActive = false;
            _glowTimer?.Dispose();
            _glowTimer = null;
        };
        _glowTimer.Start();
    }

    /// <summary>
    /// Checks if this track contains the given note number (O(1) lookup)
    /// </summary>
    public bool ContainsNote(int noteNumber) =>
        _noteNumbers?.Contains(noteNumber) ?? false;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
