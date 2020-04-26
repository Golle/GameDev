using System;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal class D3D11SwapChainBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true, EntryPoint = "D3D11SwapChainGetBuffer_")]
        internal static extern HRESULT GetBuffer(
            IntPtr swapChain,
            uint buffer,
            Guid riid,
            [Out] out IntPtr backBuffer
        );
    }
}
