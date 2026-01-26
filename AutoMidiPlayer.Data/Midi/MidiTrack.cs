using System.Linq;
using Melanchall.DryWetMidi.Core;
using Stylet;

namespace AutoMidiPlayer.Data.Midi;

public class MidiTrack
{
    private readonly IEventAggregator _events;
    private bool _isChecked;

    public MidiTrack(IEventAggregator events, TrackChunk track, int index, bool isChecked = true)
    {
        _events = events;
        _isChecked = isChecked;

        Track = track;
        Index = index;
        TrackName = track.Events.OfType<SequenceTrackNameEvent>().FirstOrDefault()?.Text;
    }

    public bool CanBePlayed => Track.Events.Count(e => e is NoteEvent) > 0;

    public int Index { get; }

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
}
