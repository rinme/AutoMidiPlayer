using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments;

/// <summary>
/// Instrument configurations for Heartopia
/// </summary>
public static class HeartopiaInstruments
{

    #region Default Key Layouts

    /// <summary>
    /// Default 2-Row keyboard key layout (15 keys, diatonic scale)
    /// Row 1 (Q, W, E, R, T, Y, U, I): C5-C6
    /// Row 2 (A, S, D, F, G, H, J): C4-B4
    /// </summary>
    private static readonly IReadOnlyList<VirtualKeyCode> Default2rLayout = new List<VirtualKeyCode>
    {
        // Row 2 (lower octave): A S D F G H J (C4-B4)
        VirtualKeyCode.VK_A,       // A (C4 - DO)
        VirtualKeyCode.VK_S,       // S (D4 - RE)
        VirtualKeyCode.VK_D,       // D (E4 - MI)
        VirtualKeyCode.VK_F,       // F (F4 - FA)
        VirtualKeyCode.VK_G,       // G (G4 - SOL)
        VirtualKeyCode.VK_H,       // H (A4 - LA)
        VirtualKeyCode.VK_J,       // J (B4 - SI)

        // Row 1 (higher octave): Q W E R T Y U I (C5-C6)
        VirtualKeyCode.VK_Q,       // Q (C5 - DO*)
        VirtualKeyCode.VK_W,       // W (D5 - RE*)
        VirtualKeyCode.VK_E,       // E (E5 - MI*)
        VirtualKeyCode.VK_R,       // R (F5 - FA*)
        VirtualKeyCode.VK_T,       // T (G5 - SOL*)
        VirtualKeyCode.VK_Y,       // Y (A5 - LA*)
        VirtualKeyCode.VK_U,       // U (B5 - SI*)
        VirtualKeyCode.VK_I        // I (C6 - DO**)
    };

    /// <summary>
    /// Default 3-Row keyboard key layout (15 keys, diatonic scale)
    /// Row 1 (Y, U, I, O, P): DO, RE, MI, FA, SOL (C4-G4)
    /// Row 2 (H, J, K, L, ;): LA, SI, DO, RE, MI (A4-E5)
    /// Row 3 (N, M, ,, ., /): FA, SOL, LA, SI, DO (F5-C6)
    /// </summary>
    private static readonly IReadOnlyList<VirtualKeyCode> Default3rLayout = new List<VirtualKeyCode>
    {
        // Row 1: Y U I O P (C4-G4)
        VirtualKeyCode.VK_Y,       // Y (C4 - DO)
        VirtualKeyCode.VK_U,       // U (D4 - RE)
        VirtualKeyCode.VK_I,       // I (E4 - MI)
        VirtualKeyCode.VK_O,       // O (F4 - FA)
        VirtualKeyCode.VK_P,       // P (G4 - SOL)

        // Row 2: H J K L ; (A4-E5)
        VirtualKeyCode.VK_H,       // H (A4 - LA)
        VirtualKeyCode.VK_J,       // J (B4 - SI)
        VirtualKeyCode.VK_K,       // K (C5 - DO)
        VirtualKeyCode.VK_L,       // L (D5 - RE)
        VirtualKeyCode.OEM_1,      // ; (E5 - MI)

        // Row 3: N M , . / (F5-C6)
        VirtualKeyCode.VK_N,       // N (F5 - FA)
        VirtualKeyCode.VK_M,       // M (G5 - SOL)
        VirtualKeyCode.OEM_COMMA,  // , (A5 - LA)
        VirtualKeyCode.OEM_PERIOD, // . (B5 - SI)
        VirtualKeyCode.OEM_2       // / (C6 - DO)
    };

    #endregion

    #region Piano Notes

    private static readonly List<int> Piano3rNotes = new()
    {
        // Row 1: C4-G4
        60, 62, 64, 65, 67,
        // Row 2: A4-E5
        69, 71, 72, 74, 76,
        // Row 3: F5-C6
        77, 79, 81, 83, 84
    };

    public static readonly InstrumentConfig Piano3r = new(
        name: "Heartopia Piano 3-Row",
        notes: Piano3rNotes,
        fixedLayout: Default3rLayout,
        usesKeyboardLayout: false
    );

    /// <summary>
    /// Piano 22 key layout (diatonic scale - white keys only)
    /// Bottom row: Z X C V B N M (C3-B3)
    /// Middle row: A S D F G H J (C4-B4)
    /// Top row: Q W E R T Y U I (C5-C6)
    /// </summary>
    private static readonly IReadOnlyList<VirtualKeyCode> Piano22kLayout = new List<VirtualKeyCode>
    {
        // Bottom row: Z X C V B N M (C3-B3)
        VirtualKeyCode.VK_Z, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M,

        // Middle row: A S D F G H J (C4-B4)
        VirtualKeyCode.VK_A, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

        // Top row: Q W E R T Y U I (C5-C6)
        VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
        VirtualKeyCode.VK_T, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U, VirtualKeyCode.VK_I
    };

    private static readonly List<int> Piano22kNotes = new()
    {
        // Bottom row: C3-B3
        48, 50, 52, 53, 55, 57, 59,
        // Middle row: C4-B4
        60, 62, 64, 65, 67, 69, 71,
        // Top row: C5-C6
        72, 74, 76, 77, 79, 81, 83, 84
    };

    public static readonly InstrumentConfig Piano22k = new(
        name: "Heartopia Piano 22",
        notes: Piano22kNotes,
        fixedLayout: Piano22kLayout,
        usesKeyboardLayout: false
    );

    /// <summary>
    /// Piano 37 keyboard key layout (full chromatic scale)
    /// Low octave:    , L . ; / O 0 P - [ = ]
    /// Middle octave: Z S X D C V G B H N J M
    /// High octave:   Q 2 W 3 E R 5 T 6 Y 7 U I
    /// </summary>
    private static readonly IReadOnlyList<VirtualKeyCode> Piano37kLayout = new List<VirtualKeyCode>
    {
        // Low octave (C3-B3): , L . ; / O 0 P - [ = ]
        VirtualKeyCode.OEM_COMMA, VirtualKeyCode.VK_L, VirtualKeyCode.OEM_PERIOD, VirtualKeyCode.OEM_1,
        VirtualKeyCode.OEM_2, VirtualKeyCode.VK_O, VirtualKeyCode.VK_0, VirtualKeyCode.VK_P,
        VirtualKeyCode.OEM_MINUS, VirtualKeyCode.OEM_4, VirtualKeyCode.OEM_PLUS, VirtualKeyCode.OEM_6,

        // Middle octave (C4-B4): Z S X D C V G B H N J M
        VirtualKeyCode.VK_Z, VirtualKeyCode.VK_S, VirtualKeyCode.VK_X, VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_C, VirtualKeyCode.VK_V, VirtualKeyCode.VK_G, VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_H, VirtualKeyCode.VK_N, VirtualKeyCode.VK_J, VirtualKeyCode.VK_M,

        // High octave (C5-C6): Q 2 W 3 E R 5 T 6 Y 7 U I
        VirtualKeyCode.VK_Q, VirtualKeyCode.VK_2, VirtualKeyCode.VK_W, VirtualKeyCode.VK_3,
        VirtualKeyCode.VK_E, VirtualKeyCode.VK_R, VirtualKeyCode.VK_5, VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_6, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_7, VirtualKeyCode.VK_U,
        VirtualKeyCode.VK_I
    };

    private static readonly List<int> Piano37kNotes = new()
    {
        // Low octave (C3-B3) - chromatic
        48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
        // Middle octave (C4-B4) - chromatic
        60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71,
        // High octave (C5-C6) - chromatic
        72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84
    };

    public static readonly InstrumentConfig Piano37k = new(
        name: "Heartopia Piano 37",
        notes: Piano37kNotes,
        fixedLayout: Piano37kLayout,
        usesKeyboardLayout: false
    );

    #endregion

    #region Lyre Notes

    private static readonly List<int> Lyre2rNotes = new()
    {
        // Row 2 (lower octave): C4-B4
        60, 62, 64, 65, 67, 69, 71,
        // Row 1 (higher octave): C5-C6
        72, 74, 76, 77, 79, 81, 83, 84
    };

    public static readonly InstrumentConfig Lyre2r = new(
        name: "Heartopia Lyre 2-Row",
        notes: Lyre2rNotes,
        fixedLayout: Default2rLayout,
        usesKeyboardLayout: false
    );


    /// <summary>
    /// Lyre 3-Row shares the same layout and notes as Piano 3-Row
    /// </summary>
    public static readonly InstrumentConfig Lyre3r = new(
        name: "Heartopia Lyre 3-Row",
        notes: Piano3rNotes,  // Same notes as Piano 3-Row
        fixedLayout: Default3rLayout,  // Same layout as Piano 3-Row
        usesKeyboardLayout: false
    );

    #endregion

    /// <summary>
    /// All Heartopia instruments
    /// </summary>
    public static readonly Dictionary<Keyboard.Instrument, InstrumentConfig> All = new()
    {
        [Keyboard.Instrument.HeartopiaPiano3r] = Piano3r,
        [Keyboard.Instrument.HeartopiaLyre3r] = Lyre3r,
        [Keyboard.Instrument.HeartopiaLyre2r] = Lyre2r,
        [Keyboard.Instrument.HeartopiaPiano22k] = Piano22k,
        [Keyboard.Instrument.HeartopiaPiano37k] = Piano37k
    };
}
