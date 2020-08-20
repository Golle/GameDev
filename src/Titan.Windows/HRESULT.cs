using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HRESULT
    {
        private IntPtr _value;
        public IntPtr Code => _value;
        public bool Failed => _value != IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Check(string className, string function)
        {
            if (Failed)
            {
                throw new Win32Exception($"{className} {function} failed with code: 0x{Code.ToString("X")}");
            }
        }
    }
}
