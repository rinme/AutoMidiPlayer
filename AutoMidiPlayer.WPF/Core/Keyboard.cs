using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CollectionNeverQueried.Global")]
public static class Keyboard
{
    public enum Instrument
    {
        GenshinWindsongLyre,
        GenshinFloralZither,
        GenshinVintageLyre,
        Heartopia15,
        Heartopia22,
        Heartopia37
    }

    public enum Layout
    {
        QWERTY,
        QWERTZ,
        AZERTY,
        DVORAK,
        DVORAKLeft,
        DVORAKRight,
        Colemak
    }

    public static readonly Dictionary<Instrument, string> InstrumentNames = new()
    {
        [Instrument.GenshinWindsongLyre] = "Windsong Lyre",
        [Instrument.GenshinFloralZither] = "Floral Zither",
        [Instrument.GenshinVintageLyre] = "Vintage Lyre",
        [Instrument.Heartopia15] = "Heartopia 15",
        [Instrument.Heartopia22] = "Heartopia Piano 22",
        [Instrument.Heartopia37] = "Heartopia Piano 37"
    };

    public static readonly Dictionary<Layout, string> LayoutNames = new()
    {
        [Layout.QWERTY] = "QWERTY",
        [Layout.QWERTZ] = "QWERTZ",
        [Layout.AZERTY] = "AZERTY",
        [Layout.DVORAK] = "DVORAK",
        [Layout.DVORAKLeft] = "DVORAK Left Handed",
        [Layout.DVORAKRight] = "DVORAK Right Handed",
        [Layout.Colemak] = "Colemak"
    };

    private static readonly IReadOnlyList<VirtualKeyCode> AZERTY = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_W,
        VirtualKeyCode.VK_X,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.OEM_COMMA,

        VirtualKeyCode.VK_Q,
        VirtualKeyCode.VK_S,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_J,

        VirtualKeyCode.VK_A,
        VirtualKeyCode.VK_Z,
        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_R,
        VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_U
    };

    private static readonly IReadOnlyList<VirtualKeyCode> Colemak = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_Z,
        VirtualKeyCode.VK_X,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_J,
        VirtualKeyCode.VK_M,

        VirtualKeyCode.VK_A,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_Y,

        VirtualKeyCode.VK_Q,
        VirtualKeyCode.VK_W,
        VirtualKeyCode.VK_K,
        VirtualKeyCode.VK_S,
        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_O,
        VirtualKeyCode.VK_I
    };

    private static readonly IReadOnlyList<VirtualKeyCode> DVORAK = new List<VirtualKeyCode>
    {
        VirtualKeyCode.OEM_2,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_I,
        VirtualKeyCode.OEM_PERIOD,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.VK_L,
        VirtualKeyCode.VK_M,

        VirtualKeyCode.VK_A,
        VirtualKeyCode.OEM_1,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_U,
        VirtualKeyCode.VK_J,
        VirtualKeyCode.VK_C,

        VirtualKeyCode.VK_X,
        VirtualKeyCode.OEM_COMMA,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_O,
        VirtualKeyCode.VK_K,
        VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_F
    };

    private static readonly IReadOnlyList<VirtualKeyCode> DVORAKLeft = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_L,
        VirtualKeyCode.VK_X,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.VK_6,

        VirtualKeyCode.VK_K,
        VirtualKeyCode.VK_U,
        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_5,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_8,

        VirtualKeyCode.VK_W,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_J,
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_R,
        VirtualKeyCode.VK_T
    };

    private static readonly IReadOnlyList<VirtualKeyCode> DVORAKRight = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_L,
        VirtualKeyCode.OEM_COMMA,
        VirtualKeyCode.VK_P,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.VK_7,

        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_U,
        VirtualKeyCode.VK_K,
        VirtualKeyCode.VK_8,
        VirtualKeyCode.OEM_PERIOD,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_5,

        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_M,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_J,
        VirtualKeyCode.VK_O,
        VirtualKeyCode.VK_I
    };

    private static readonly IReadOnlyList<VirtualKeyCode> QWERTY = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_Z,
        VirtualKeyCode.VK_X,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.VK_M,

        VirtualKeyCode.VK_A,
        VirtualKeyCode.VK_S,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_J,

        VirtualKeyCode.VK_Q,
        VirtualKeyCode.VK_W,
        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_R,
        VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_U
    };

    private static readonly IReadOnlyList<VirtualKeyCode> QWERTZ = new List<VirtualKeyCode>
    {
        VirtualKeyCode.VK_Y,
        VirtualKeyCode.VK_X,
        VirtualKeyCode.VK_C,
        VirtualKeyCode.VK_V,
        VirtualKeyCode.VK_B,
        VirtualKeyCode.VK_N,
        VirtualKeyCode.VK_M,

        VirtualKeyCode.VK_A,
        VirtualKeyCode.VK_S,
        VirtualKeyCode.VK_D,
        VirtualKeyCode.VK_F,
        VirtualKeyCode.VK_G,
        VirtualKeyCode.VK_H,
        VirtualKeyCode.VK_J,

        VirtualKeyCode.VK_Q,
        VirtualKeyCode.VK_W,
        VirtualKeyCode.VK_E,
        VirtualKeyCode.VK_R,
        VirtualKeyCode.VK_T,
        VirtualKeyCode.VK_Z,
        VirtualKeyCode.VK_U
    };

    // Heartopia 15 key layout (diatonic scale - white keys only)
    // Row 1 (Y, U, I, O, P): DO, RE, MI, FA, SOL (C4, D4, E4, F4, G4)
    // Row 2 (H, J, K, L, ;): LA, SI, DO, RE, MI (A4, B4, C5, D5, E5)
    // Row 3 (N, M, ,, ., /): FA, SOL, LA, SI, DO (F5, G5, A5, B5, C6)
    private static readonly IReadOnlyList<VirtualKeyCode> Heartopia15Layout = new List<VirtualKeyCode>
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

    // Heartopia 15 notes (diatonic scale - white keys only, C4-C6)
    private static readonly List<int> Heartopia15Notes = new()
    {
        // Row 1: C4-G4
        60, // C4
        62, // D4
        64, // E4
        65, // F4
        67, // G4

        // Row 2: A4-E5
        69, // A4
        71, // B4
        72, // C5
        74, // D5
        76, // E5

        // Row 3: F5-C6
        77, // F5
        79, // G5
        81, // A5
        83, // B5
        84  // C6 (15th key)
    };

    // Heartopia Piano 22 key layout (diatonic scale - white keys only)
    private static readonly IReadOnlyList<VirtualKeyCode> Heartopia22Layout = new List<VirtualKeyCode>
    {
        // Bottom row: Z X C V B N M (C3-B3) - dots below numbers
        VirtualKeyCode.VK_Z,       // Z (C3 - DO)
        VirtualKeyCode.VK_X,       // X (D3 - RE)
        VirtualKeyCode.VK_C,       // C (E3 - MI)
        VirtualKeyCode.VK_V,       // V (F3 - FA)
        VirtualKeyCode.VK_B,       // B (G3 - SOL)
        VirtualKeyCode.VK_N,       // N (A3 - LA)
        VirtualKeyCode.VK_M,       // M (B3 - SI)

        // Middle row: A S D F G H J (C4-B4) - no dots
        VirtualKeyCode.VK_A,       // A (C4 - DO)
        VirtualKeyCode.VK_S,       // S (D4 - RE)
        VirtualKeyCode.VK_D,       // D (E4 - MI)
        VirtualKeyCode.VK_F,       // F (F4 - FA)
        VirtualKeyCode.VK_G,       // G (G4 - SOL)
        VirtualKeyCode.VK_H,       // H (A4 - LA)
        VirtualKeyCode.VK_J,       // J (B4 - SI)

        // Top row: Q W E R T Y U I (C5-C6) - dots above numbers
        VirtualKeyCode.VK_Q,       // Q (C5 - DO)
        VirtualKeyCode.VK_W,       // W (D5 - RE)
        VirtualKeyCode.VK_E,       // E (E5 - MI)
        VirtualKeyCode.VK_R,       // R (F5 - FA)
        VirtualKeyCode.VK_T,       // T (G5 - SOL)
        VirtualKeyCode.VK_Y,       // Y (A5 - LA)
        VirtualKeyCode.VK_U,       // U (B5 - SI)
        VirtualKeyCode.VK_I        // I (C6 - DO)
    };

    // Heartopia Piano 22 notes (diatonic scale - white keys only, C3-C6)
    private static readonly List<int> Heartopia22Notes = new()
    {
        // Bottom row: C3-B3 (MIDI 48-59, white keys only)
        48, // C3  [0] Z
        50, // D3  [1] X
        52, // E3  [2] C
        53, // F3  [3] V
        55, // G3  [4] B
        57, // A3  [5] N
        59, // B3  [6] M

        // Middle row: C4-B4 (MIDI 60-71, white keys only)
        60, // C4  [7] A
        62, // D4  [8] S
        64, // E4  [9] D
        65, // F4 [10] F
        67, // G4 [11] G
        69, // A4 [12] H
        71, // B4 [13] J

        // Top row: C5-C6 (MIDI 72-84, white keys only)
        72, // C5 [14] Q
        74, // D5 [15] W
        76, // E5 [16] E
        77, // F5 [17] R
        79, // G5 [18] T
        81, // A5 [19] Y
        83, // B5 [20] U
        84  // C6 [21] I
    };

    // Heartopia Piano 37 key layout (full chromatic scale)
    // Low octave:    ,  L  .  ;  /  O  0  P  -  [  =  ]
    // Middle octave: Z  S  X  D  C  V  G  B  H  N  J  M
    // High octave:   Q  2  W  3  E  R  5  T  6  Y  7  U  I
    private static readonly IReadOnlyList<VirtualKeyCode> HeartopiaPiano37Layout = new List<VirtualKeyCode>
    {
        // Low octave (C3-B3): , L . ; / O 0 P - [ = ]
        VirtualKeyCode.OEM_COMMA,  // , (C3)
        VirtualKeyCode.VK_L,       // L (C#3)
        VirtualKeyCode.OEM_PERIOD, // . (D3)
        VirtualKeyCode.OEM_1,      // ; (D#3)
        VirtualKeyCode.OEM_2,      // / (E3)
        VirtualKeyCode.VK_O,       // O (F3)
        VirtualKeyCode.VK_0,       // 0 (F#3)
        VirtualKeyCode.VK_P,       // P (G3)
        VirtualKeyCode.OEM_MINUS,  // - (G#3)
        VirtualKeyCode.OEM_4,      // [ (A3)
        VirtualKeyCode.OEM_PLUS,   // = (A#3)
        VirtualKeyCode.OEM_6,      // ] (B3)

        // Middle octave (C4-B4): Z S X D C V G B H N J M
        VirtualKeyCode.VK_Z,       // Z (C4)
        VirtualKeyCode.VK_S,       // S (C#4)
        VirtualKeyCode.VK_X,       // X (D4)
        VirtualKeyCode.VK_D,       // D (D#4)
        VirtualKeyCode.VK_C,       // C (E4)
        VirtualKeyCode.VK_V,       // V (F4)
        VirtualKeyCode.VK_G,       // G (F#4)
        VirtualKeyCode.VK_B,       // B (G4)
        VirtualKeyCode.VK_H,       // H (G#4)
        VirtualKeyCode.VK_N,       // N (A4)
        VirtualKeyCode.VK_J,       // J (A#4)
        VirtualKeyCode.VK_M,       // M (B4)

        // High octave (C5-C6): Q 2 W 3 E R 5 T 6 Y 7 U I
        VirtualKeyCode.VK_Q,       // Q (C5)
        VirtualKeyCode.VK_2,       // 2 (C#5)
        VirtualKeyCode.VK_W,       // W (D5)
        VirtualKeyCode.VK_3,       // 3 (D#5)
        VirtualKeyCode.VK_E,       // E (E5)
        VirtualKeyCode.VK_R,       // R (F5)
        VirtualKeyCode.VK_5,       // 5 (F#5)
        VirtualKeyCode.VK_T,       // T (G5)
        VirtualKeyCode.VK_6,       // 6 (G#5)
        VirtualKeyCode.VK_Y,       // Y (A5)
        VirtualKeyCode.VK_7,       // 7 (A#5)
        VirtualKeyCode.VK_U,       // U (B5)
        VirtualKeyCode.VK_I        // I (C6) - 37th key
    };

    private static readonly List<int> DefaultNotes = new()
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
    };

    private static readonly List<int> VintageNotes = new()
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
    };

    // Heartopia Piano 37 - Full chromatic scale (37 keys)
    private static readonly List<int> HeartopiaNotes = new()
    {
        // Low octave (C3-B3)
        48, // C3
        49, // C#3
        50, // D3
        51, // D#3
        52, // E3
        53, // F3
        54, // F#3
        55, // G3
        56, // G#3
        57, // A3
        58, // A#3
        59, // B3

        // Middle octave (C4-B4)
        60, // C4
        61, // C#4
        62, // D4
        63, // D#4
        64, // E4
        65, // F4
        66, // F#4
        67, // G4
        68, // G#4
        69, // A4
        70, // A#4
        71, // B4

        // High octave (C5-C6)
        72, // C5
        73, // C#5
        74, // D5
        75, // D#5
        76, // E5
        77, // F5
        78, // F#5
        79, // G5
        80, // G#5
        81, // A5
        82, // A#5
        83, // B5
        84  // C6 (37th key)
    };

    public static IEnumerable<VirtualKeyCode> GetLayout(Layout layout, Instrument instrument) => instrument switch
    {
        // Heartopia Piano has fixed keys regardless of keyboard layout
        Instrument.Heartopia15 => Heartopia15Layout,
        Instrument.Heartopia22 => Heartopia22Layout,
        Instrument.Heartopia37 => HeartopiaPiano37Layout,
        // Other instruments use the selected keyboard layout
        _ => layout switch
        {
            Layout.QWERTY => QWERTY,
            Layout.QWERTZ => QWERTZ,
            Layout.AZERTY => AZERTY,
            Layout.DVORAK => DVORAK,
            Layout.DVORAKLeft => DVORAKLeft,
            Layout.DVORAKRight => DVORAKRight,
            Layout.Colemak => Colemak,
            _ => QWERTY
        }
    };

    public static IList<int> GetNotes(Instrument instrument) => instrument switch
    {
        Instrument.GenshinWindsongLyre => DefaultNotes,
        Instrument.GenshinFloralZither => DefaultNotes,
        Instrument.GenshinVintageLyre => VintageNotes,
        Instrument.Heartopia15 => Heartopia15Notes,
        Instrument.Heartopia22 => Heartopia22Notes,
        Instrument.Heartopia37 => HeartopiaNotes,
        _ => DefaultNotes
    };
}
