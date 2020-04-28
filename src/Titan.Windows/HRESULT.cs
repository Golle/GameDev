using System;
using System.Runtime.InteropServices;

namespace Titan.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HRESULT
    {
        private IntPtr _value;
        public IntPtr Code => _value;
        public bool Failed => _value != IntPtr.Zero;
    }
}
