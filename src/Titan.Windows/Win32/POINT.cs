using System.Runtime.InteropServices;

namespace Titan.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        internal long X;
        internal long Y;
    }
}
