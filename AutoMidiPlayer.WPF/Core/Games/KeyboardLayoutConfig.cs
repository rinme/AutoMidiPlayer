using System.Collections.Generic;
using WindowsInput.Native;

namespace AutoMidiPlayer.WPF.Core.Instruments;

public class KeyboardLayoutConfig
{
    public string Name { get; }

    public IReadOnlyList<VirtualKeyCode> Keys { get; }

    public KeyboardLayoutConfig(string name, IReadOnlyList<VirtualKeyCode> keys)
    {
        Name = name;
        Keys = keys;
    }
}
