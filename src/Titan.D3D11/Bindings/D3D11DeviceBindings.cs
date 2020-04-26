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
            IntPtr adapter, 
            D3D_DRIVER_TYPE driverType, 
            IntPtr hModule, 
            uint flags, 
            D3D_FEATURE_LEVEL[] featureLevel, 
            uint featureLevels, 
            uint sdkVerion, 
            [In] in DXGI_SWAP_CHAIN_DESC pSwapChainDesc,
            [Out] out IntPtr swapChain,
            [Out] out IntPtr device,
            [Out] out D3D_FEATURE_LEVEL pFeatureLevel,
            [Out] out IntPtr context
            );
    }
}
