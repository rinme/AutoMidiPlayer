using System;

namespace AutoMidiPlayer.Data.Entities;

public class History
{
    protected History() { }

    public History(string path, int key)
    {
        Key = key;
        Path = path;
    }

    public Guid Id { get; set; }

    public int Key { get; set; }

    public string Path { get; set; } = null!;

    public Transpose? Transpose { get; set; }

    /// Playback speed (0.1 to 4.0).
    public double? Speed { get; set; }

    /// Comma-separated list of disabled track indices (0-based).
    public string? DisabledTracks { get; set; }
}
