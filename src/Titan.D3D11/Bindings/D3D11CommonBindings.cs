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

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3DReadFileToBlob_(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pFileName,
            [Out] out IntPtr ppContents
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3DWriteBlobToFile_(
            [In] IntPtr pBlob,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pFileName,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOverwrite
        );
    }
}
