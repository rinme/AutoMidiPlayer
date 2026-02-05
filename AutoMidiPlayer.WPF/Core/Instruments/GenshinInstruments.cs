using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments;

/// <summary>
/// Instrument configurations for Genshin Impact
/// </summary>
public static class GenshinInstruments
{
    /// <summary>
    /// Windsong Lyre - 21 keys, diatonic scale (C3-B5)
    /// </summary>
    public static readonly InstrumentConfig WindsongLyre = new(
        name: "Windsong Lyre",
        notes: new List<int>
        {
            48, // C3
            50, // D3
            52, // E3
            53, // F3
            55, // G3
            57, // A3
            59, // B3

            60, // C4
            62, // D4
            64, // E4
            65, // F4
            67, // G4
            69, // A4
            71, // B4

            72, // C5
            74, // D5
            76, // E5
            77, // F5
            79, // G5
            81, // A5
            83  // B5
        },
        usesKeyboardLayout: true
    );

    /// <summary>
    /// Floral Zither - 21 keys, diatonic scale (same as Windsong Lyre)
    /// </summary>
    public static readonly InstrumentConfig FloralZither = new(
        name: "Floral Zither",
        notes: new List<int>
        {
            48, // C3
            50, // D3
            52, // E3
            53, // F3
            55, // G3
            57, // A3
            59, // B3

            60, // C4
            62, // D4
            64, // E4
            65, // F4
            67, // G4
            69, // A4
            71, // B4

            72, // C5
            74, // D5
            76, // E5
            77, // F5
            79, // G5
            81, // A5
            83  // B5
        },
        usesKeyboardLayout: true
    );

    /// <summary>
    /// Vintage Lyre - 21 keys, Dorian mode scale
    /// </summary>
    public static readonly InstrumentConfig VintageLyre = new(
        name: "Vintage Lyre",
        notes: new List<int>
        {
            48, // C3
            50, // D3
            51, // Eb3
            53, // F3
            55, // G3
            57, // A3
            58, // Bb3

            60, // C4
            62, // D4
            63, // Eb4
            65, // F4
            67, // G4
            69, // A4
            70, // Bb4

            72, // C5
            74, // Db5
            76, // Eb5
            77, // F5
            79, // G5
            80, // Ab5
            82  // Bb5
        },
        usesKeyboardLayout: true
    );

    /// <summary>
    /// All Genshin Impact instruments
    /// </summary>
    public static readonly Dictionary<Keyboard.Instrument, InstrumentConfig> All = new()
    {
        [Keyboard.Instrument.GenshinWindsongLyre] = WindsongLyre,
        [Keyboard.Instrument.GenshinFloralZither] = FloralZither,
        [Keyboard.Instrument.GenshinVintageLyre] = VintageLyre
    };
}
