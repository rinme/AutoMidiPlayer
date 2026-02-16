using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments
{

    /// <summary>
    /// Genshin keyboard layouts.
    /// </summary>
    internal static class GenshinKeyboardLayouts
    {
        public static readonly KeyboardLayoutConfig QWERTY = new(
            name: "QWERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_Z, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_A, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U
            });

        public static readonly KeyboardLayoutConfig QWERTZ = new(
            name: "QWERTZ",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_Y, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_A, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_Z, VirtualKeyCode.VK_U
            });

        public static readonly KeyboardLayoutConfig AZERTY = new(
            name: "AZERTY",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_W, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.OEM_COMMA,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_S, VirtualKeyCode.VK_D, VirtualKeyCode.VK_F,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J,

                VirtualKeyCode.VK_A, VirtualKeyCode.VK_Z, VirtualKeyCode.VK_E, VirtualKeyCode.VK_R,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_Y, VirtualKeyCode.VK_U
            });

        public static readonly KeyboardLayoutConfig DVORAK = new(
            name: "DVORAK",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.OEM_2, VirtualKeyCode.VK_B, VirtualKeyCode.VK_I, VirtualKeyCode.OEM_PERIOD,
                VirtualKeyCode.VK_N, VirtualKeyCode.VK_L, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_A, VirtualKeyCode.OEM_1, VirtualKeyCode.VK_H, VirtualKeyCode.VK_Y,
                VirtualKeyCode.VK_U, VirtualKeyCode.VK_J, VirtualKeyCode.VK_C,

                VirtualKeyCode.VK_X, VirtualKeyCode.OEM_COMMA, VirtualKeyCode.VK_D, VirtualKeyCode.VK_O,
                VirtualKeyCode.VK_K, VirtualKeyCode.VK_T, VirtualKeyCode.VK_F
            });

        public static readonly KeyboardLayoutConfig DVORAKLeft = new(
            name: "DVORAKLeft",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_L, VirtualKeyCode.VK_X, VirtualKeyCode.VK_D, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_E, VirtualKeyCode.VK_N, VirtualKeyCode.VK_6,

                VirtualKeyCode.VK_K, VirtualKeyCode.VK_U, VirtualKeyCode.VK_F, VirtualKeyCode.VK_5,
                VirtualKeyCode.VK_C, VirtualKeyCode.VK_H, VirtualKeyCode.VK_8,

                VirtualKeyCode.VK_W, VirtualKeyCode.VK_B, VirtualKeyCode.VK_J, VirtualKeyCode.VK_Y,
                VirtualKeyCode.VK_G, VirtualKeyCode.VK_R, VirtualKeyCode.VK_T
            });

        public static readonly KeyboardLayoutConfig DVORAKRight = new(
            name: "DVORAKRight",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_D, VirtualKeyCode.VK_C, VirtualKeyCode.VK_L, VirtualKeyCode.OEM_COMMA,
                VirtualKeyCode.VK_P, VirtualKeyCode.VK_N, VirtualKeyCode.VK_7,

                VirtualKeyCode.VK_F, VirtualKeyCode.VK_U, VirtualKeyCode.VK_K, VirtualKeyCode.VK_8,
                VirtualKeyCode.OEM_PERIOD, VirtualKeyCode.VK_H, VirtualKeyCode.VK_5,

                VirtualKeyCode.VK_E, VirtualKeyCode.VK_M, VirtualKeyCode.VK_G, VirtualKeyCode.VK_Y,
                VirtualKeyCode.VK_J, VirtualKeyCode.VK_O, VirtualKeyCode.VK_I
            });

        public static readonly KeyboardLayoutConfig Colemak = new(
            name: "Colemak",
            keys: new List<VirtualKeyCode>
            {
                VirtualKeyCode.VK_Z, VirtualKeyCode.VK_X, VirtualKeyCode.VK_C, VirtualKeyCode.VK_V,
                VirtualKeyCode.VK_B, VirtualKeyCode.VK_J, VirtualKeyCode.VK_M,

                VirtualKeyCode.VK_A, VirtualKeyCode.VK_D, VirtualKeyCode.VK_G, VirtualKeyCode.VK_E,
                VirtualKeyCode.VK_T, VirtualKeyCode.VK_H, VirtualKeyCode.VK_Y,

                VirtualKeyCode.VK_Q, VirtualKeyCode.VK_W, VirtualKeyCode.VK_K, VirtualKeyCode.VK_S,
                VirtualKeyCode.VK_F, VirtualKeyCode.VK_O, VirtualKeyCode.VK_I
            });
    }
}
