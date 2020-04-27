using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    public static class D3D11CommonBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern ulong ReleaseComObject(IntPtr unknown);

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT QueryInterface_(
            [In] IntPtr unknown,
            [In] Guid iid,
            [Out] out IntPtr ppObject
        );


        [DllImport(Constants.D3D11Dll)]
        public static extern uint D3D11SdkVersion();
    }
}
