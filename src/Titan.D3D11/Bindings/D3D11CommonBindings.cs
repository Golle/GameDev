using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings
{
    public static class D3D11CommonBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern ulong ReleaseComObject(IntPtr unknown);
        
        [DllImport(Constants.D3D11Dll)]
        public static extern uint D3D11SdkVersion();
    }
}
