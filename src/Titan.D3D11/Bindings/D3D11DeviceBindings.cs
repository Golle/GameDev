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
            [In] IntPtr adapter,
            [In] D3DDriverType driverType,
            [In] IntPtr hModule,
            [In] uint flags,
            [In] D3DFeatureLevel[] featureLevel,
            [In] uint featureLevels,
            [In] uint sdkVersion,
            [Out] out IntPtr device, 
            [Out] out D3DFeatureLevel pFeatureLevel,
            [Out] out IntPtr context
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern unsafe HRESULT D3D11CreateDeviceAndSwapChain_(
            [In] IntPtr adapter,
            [In] D3DDriverType driverType,
            [In] IntPtr hModule,
            [In] D3D11CreateDeviceFlag flags,
            [In] D3DFeatureLevel* featureLevel,
            [In] uint featureLevels,
            [In] uint sdkVerion, 
            [In] in DxgiSwapChainDesc pSwapChainDesc,
            [Out] out IntPtr swapChain,
            [Out] out IntPtr device,
            [Out] out D3DFeatureLevel pFeatureLevel,
            [Out] out IntPtr context
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern unsafe HRESULT D3D11CreateRenderTargetView_(
            [In] IntPtr device,
            [In] IntPtr pResource,
            [In] D3D11_RENDER_TARGET_VIEW_DESC* pDesc,
            [Out] out IntPtr renderTargetView
        );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern unsafe HRESULT D3D11CreateBuffer_(
            [In] IntPtr device,
            [In] D3D11BufferDesc* pDesc,
            [In] D3D11_SUBRESOURCE_DATA* pInitialData,
            [Out] out IntPtr ppBUffer
        );
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct D3D11_SUBRESOURCE_DATA
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 20)] // Size = 20 is in 64 bit architecture, wont work in x86.
    internal struct D3D11_RENDER_TARGET_VIEW_DESC
    {
        // this struct has union, can use FieldOffsetAttribute to mimic that behavior. For now we'll leave it at null.
    }
}
