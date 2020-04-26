using System;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal static class D3D11DeviceBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3D11CreateDeviceBinding_(
            IntPtr adapter, 
            D3D_DRIVER_TYPE driverType, 
            IntPtr hModule, 
            uint flags,
            D3D_FEATURE_LEVEL[] featureLevel,
            uint featureLevels, 
            uint sdkVersion,
            [Out] out IntPtr device, 
            [Out] out D3D_FEATURE_LEVEL pFeatureLevel,
            [Out] out IntPtr context
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern HRESULT D3D11CreateDeviceAndSwapChain_(
            [In] IntPtr adapter,
            [In] D3D_DRIVER_TYPE driverType,
            [In] IntPtr hModule,
            [In] uint flags,
            [In] D3D_FEATURE_LEVEL[] featureLevel,
            [In] uint featureLevels,
            [In] uint sdkVerion, 
            [In] in DXGI_SWAP_CHAIN_DESC pSwapChainDesc,
            [Out] out IntPtr swapChain,
            [Out] out IntPtr device,
            [Out] out D3D_FEATURE_LEVEL pFeatureLevel,
            [Out] out IntPtr context
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern unsafe HRESULT D3D11CreateRenderTargetView_(
            [In] IntPtr device,
            [In] IntPtr pResource,
            [In] D3D11_RENDER_TARGET_VIEW_DESC* pDesc,
            [Out] out IntPtr renderTargetView
        );
    }

    [StructLayout(LayoutKind.Explicit, Size = 20)] // Size = 20 is in 64 bit architecture, wont work in x86.
    internal struct D3D11_RENDER_TARGET_VIEW_DESC
    {
        // this struct has union, can use FieldOffsetAttribute to mimic that behavior. For now we'll leave it at null.
    }
}
