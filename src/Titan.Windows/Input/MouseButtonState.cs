using System;

namespace Titan.Windows.Input
{
    [Flags]
    internal enum MouseButtonState
    {
        None = 0,
        Left = 0x0001,
        Right = 0x0002,
        Middle = 0x0010,
        Shift = 0x0004,
        Control = 0x0008,

        XButton1 = 0x0020,
        XButton2 = 0x0040
    }
}
