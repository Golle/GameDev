using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal class D3D11SwapChainBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern HRESULT GetBuffer_(
            [In] IntPtr swapChain,
            [In] uint buffer,
            [In] Guid riid,
            [Out] out IntPtr backBuffer
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void Present_(
            [In] IntPtr swapChain,
            [In] uint syncInterval,
            [In] uint flags
        );
    }
}
