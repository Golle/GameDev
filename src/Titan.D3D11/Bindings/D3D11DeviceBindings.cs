using System;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal static class D3D11DeviceBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3D11CreateDevice_(
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

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
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

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateRenderTargetView_(
            [In] IntPtr device,
            [In] IntPtr pResource,
            [In] D3D11RenderTargetViewDesc* pDesc,
            [Out] out IntPtr renderTargetView
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateBuffer_(
            [In] IntPtr device,
            [In] D3D11BufferDesc* pDesc,
            [In] D3D11SubresourceData* pInitialData,
            [Out] out IntPtr ppBUffer
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateVertexShader_(
            [In] IntPtr device,
            [In] IntPtr blob,
            [In] UIntPtr byteCodeLength,
            [In] IntPtr pClassLinkage,
            [Out] out IntPtr ppVertexShader
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreatePixelShader_(
            [In] IntPtr device,
            [In] IntPtr blob,
            [In] UIntPtr byteCodeLength,
            [In] IntPtr pClassLinkage,
            [Out] out IntPtr ppPixelShader
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateInputLayout_(
            [In] IntPtr device,
            [In] D3D11InputElementDesc[] inputElementDescs,
            [In] uint numElements,
            [In] IntPtr compiledShader,
            [In] UIntPtr byteCodeLength,
            [Out] out IntPtr ppInputLayout
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateDepthStencilState_(
            [In] IntPtr device,
            [In] D3D11DepthStencilDesc* pDepthStencilDesc,
            [Out] out IntPtr ppDepthStencilState
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateTexture2D_(
            [In] IntPtr device,
            [In] D3D11Texture2DDesc* pDesc,
            [In] D3D11SubresourceData* pInitialData,
            [Out] out IntPtr ppTexture2D
        );
        [DllImport(Constants.D3D11Dll, CallingConvention =CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateDepthStencilView_(
            [In] IntPtr device,
            [In] IntPtr pResource,
            [In] D3D11DepthStencilViewDesc* pDesc,
            [Out] out IntPtr ppDepthStencilView
        );


        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateSamplerState_(
            [In] IntPtr device,
            [In] in D3D11SamplerDesc samplerDesc,
            [Out] out IntPtr ppSamplerState
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateShaderResourceView_(
            [In] IntPtr device,
            [In] IntPtr resource,
            [In] in D3D11ShaderResourceViewDesc desc,
            [Out] out IntPtr ppShaderResourceView
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateBlendState_(
            [In] IntPtr device,
            [In] in D3D11BlendDesc desc,
            [Out] out IntPtr ppBlendState
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT CreateDeferredContext_(
            [In] IntPtr device,
            [In] uint contextFlags,
            [Out] out IntPtr ppDeferredContext
        );
    }
}
