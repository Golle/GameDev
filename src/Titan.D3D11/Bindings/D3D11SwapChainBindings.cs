using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal class D3D11SwapChainBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true, EntryPoint = "D3D11SwapChainGetBuffer_")]
        internal static extern HRESULT GetBuffer(
            [In] IntPtr swapChain,
            [In] uint buffer,
            [In] Guid riid,
            [Out] out IntPtr backBuffer
        );


        [DllImport(Constants.D3D11Dll, SetLastError = true, EntryPoint = "SwapChainPresent_")]
        public static extern void Present(
            [In] IntPtr swapChain,
            [In] uint syncInterval,
            [In] uint flags
            );
    }

}
