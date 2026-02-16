using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments
{

    /// <summary>
    /// Heartopia keyboard layouts.
    /// </summary>
    internal static class HeartopiaKeyboardLayouts
    {
        public static readonly KeyboardLayoutConfig QWERTY_2Row = new(
            name: "QWERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_A, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U, VirtualKeyCode.VK_I
            });

        public static readonly KeyboardLayoutConfig QWERTY_3Row = new(
            name: "QWERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U, VirtualKeyCode.VK_I, VirtualKeyCode.VK_O, VirtualKeyCode.VK_P,
                VirtualKeyCode.VK_H, VirtualKeyCode.VK_J, VirtualKeyCode.VK_K, VirtualKeyCode.VK_L, VirtualKeyCode.OEM_1,
                VirtualKeyCode.VK_N, VirtualKeyCode.VK_M, VirtualKeyCode.OEM_COMMA, VirtualKeyCode.OEM_PERIOD, VirtualKeyCode.OEM_2
            });

        public static readonly KeyboardLayoutConfig QWERTY_22Key = new(
            name: "QWERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_Z, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_A, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U, VirtualKeyCode.VK_I
            });

        public static readonly KeyboardLayoutConfig QWERTY_37Key = new(
            name: "QWERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.OEM_COMMA, VirtualKeyCode.VK_L, VirtualKeyCode.OEM_PERIOD, VirtualKeyCode.OEM_1,
                VirtualKeyCode.OEM_2, VirtualKeyCode.VK_O, VirtualKeyCode.VK_0, VirtualKeyCode.VK_P,
                VirtualKeyCode.OEM_MINUS, VirtualKeyCode.OEM_4, VirtualKeyCode.OEM_PLUS, VirtualKeyCode.OEM_6,

                VirtualKeyCode.VK_Z, VirtualKeyCode.VK_S, VirtualKeyCode.VK_X, VirtualKeyCode.VK_D,
                VirtualKeyCode.VK_C, VirtualKeyCode.VK_V, VirtualKeyCode.VK_G, VirtualKeyCode.VK_B,
                VirtualKeyCode.VK_H, VirtualKeyCode.VK_N, VirtualKeyCode.VK_J, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_2, VirtualKeyCode.VK_W, VirtualKeyCode.VK_3,
                VirtualKeyCode.VK_E, VirtualKeyCode.VK_R, VirtualKeyCode.VK_5, VirtualKeyCode.VK_T,
                VirtualKeyCode.VK_6, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_7, VirtualKeyCode.VK_U,
                VirtualKeyCode.VK_I
            });
    }
}
