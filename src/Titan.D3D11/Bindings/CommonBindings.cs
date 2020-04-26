using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings
{
    internal static class CommonBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern ulong ReleaseComObject(IntPtr unknown);
    }
}
