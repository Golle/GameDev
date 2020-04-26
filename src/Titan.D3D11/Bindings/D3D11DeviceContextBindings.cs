using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings
{
    internal class D3D11DeviceContextBindings
    {
        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextOMSetRenderTargets_(
            [In] IntPtr context,
            [In] uint numViews, 
            [In] IntPtr[] ppRenderTargetViews, 
            [In] IntPtr pDepthStencilView);

        [DllImport(Constants.D3D11Dll, SetLastError = true, EntryPoint = "DeviceContextClearRenderTargetView_")]
        public static extern void ClearRenderTargetView(
            [In] IntPtr context, 
            [In] IntPtr pRenderTargetView, 
            [In] Color color);
    }
}
