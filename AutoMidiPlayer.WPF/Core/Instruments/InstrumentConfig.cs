using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments;

/// <summary>
/// Configuration for a game instrument including its notes and optional fixed layout
/// </summary>
public class InstrumentConfig
{
    /// <summary>
    /// Display name of the instrument
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// MIDI note numbers this instrument can play
    /// </summary>
    public IList<int> Notes { get; }

    /// <summary>
    /// Fixed key layout for instruments that don't use keyboard layout selection.
    /// Null if the instrument uses the keyboard layout setting.
    /// </summary>
    public IReadOnlyList<VirtualKeyCode>? FixedLayout { get; }

    /// <summary>
    /// Whether this instrument uses the keyboard layout selection (QWERTY, AZERTY, etc.)
    /// If false, the instrument has a fixed key layout that doesn't change.
    /// </summary>
    public bool UsesKeyboardLayout { get; }

    public InstrumentConfig(
        string name,
        IList<int> notes,
        IReadOnlyList<VirtualKeyCode>? fixedLayout = null,
        bool usesKeyboardLayout = true)
    {
        Name = name;
        Notes = notes;
        FixedLayout = fixedLayout;
        UsesKeyboardLayout = usesKeyboardLayout;
    }
}
