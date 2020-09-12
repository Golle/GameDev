using System;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    internal class D3D11DeviceContextBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe void OMSetRenderTargets_(
            [In] IntPtr context,
            [In] uint numViews,
            [In] IntPtr* ppRenderTargetViews,
            [In] IntPtr pDepthStencilView);

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void ClearRenderTargetView_(
            [In] IntPtr context,
            [In] IntPtr pRenderTargetView,
            [In] Color color
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void ClearDepthStencilView_(
            [In] IntPtr context,
            [In] IntPtr pDepthStencilView,
            [In] D3D11ClearFlag clearFlags,
            [In] float depth,
            [In] sbyte stencil
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void IASetVertexBuffers_(
            [In] IntPtr context,
            [In] uint startSlot,
            [In] uint numBuffers,
            [In] in IntPtr ppVertexbuffers, // this is a list of buffers, replace with IntPtr[] when we need it
            [In] in uint pStrides,
            [In] in uint pOffsets
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void Draw_(
            [In] IntPtr context,
            [In] uint vertexCount,
            [In] uint startLocation
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void VSSetShader_(
            [In] IntPtr context,
            [In] IntPtr pVertexShader,
            [In] in IntPtr ppClassInstances,
            [In] uint numClassInstances
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void PSSetShader_(
            [In] IntPtr context,
            [In] IntPtr pPixelShader,
            [In] in IntPtr ppClassInstances,
            [In] uint numClassInstances
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void PSSetShaderResources_(
            [In] IntPtr context,
            [In] uint startSlot,
            [In] uint numViews, 
            [In] in IntPtr shaderResourceViews // TODO: add support for arrays later
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void PSSetSamplers_(
            [In] IntPtr context,
            [In] uint startSlot,
            [In] uint numSamplers, 
            [In] in IntPtr samplers// TODO: add support for arrays later
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe void RSSetViewports_(
            [In] IntPtr context,
            [In] uint numViewports,
            [In] D3D11Viewport* viewports
        );


        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void IASetPrimitiveTopology_(
            [In] IntPtr context,
            [In] D3D11PrimitiveTopology topology
        );


        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void IASetInputLayout_(
            [In] IntPtr context,
            [In] IntPtr inputLayout
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void IASetIndexBuffer_(
            [In] IntPtr handle,
            [In] IntPtr indexBuffer,
            [In] DxgiFormat format,
            [In] uint offset
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void VSSetConstantBuffers_(
            [In] IntPtr handle,
            [In] uint startSlot,
            [In] uint numBuffers,
            [In] in IntPtr ppConstantBuffers // this is a list of buffers, replace with IntPtr[] when we need it
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe void VSSetShaderResources_(
            [In] IntPtr context,
            [In] uint startSlot,
            [In] uint numViews,
            [In] IntPtr* ppShaderResourceViews
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void PSSetConstantBuffers_(
            [In] IntPtr handle,
            [In] uint startSlot,
            [In] uint numBuffers,
            [In] in IntPtr ppConstantBuffers // this is a list of buffers, replace with IntPtr[] when we need it
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void OMSetDepthStencilState_(
            [In] IntPtr handle,
            [In] IntPtr pDepthStencilState,
            [In] uint stencilRef
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void OMSetBlendState_(
            [In] IntPtr handle,
            [In] IntPtr blendState,
            [In] in Color blendFactor,
            [In] uint sampleMask
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void DrawIndexed_(
            [In] IntPtr context,
            [In] uint indexCount,
            [In] uint startIndexLocation,
            [In] int baseVertexLocation
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe void UpdateSubresource_(
            [In] IntPtr context,
            [In] IntPtr pDstResource, // ID3D11Resource
            [In] uint dstSubresource,
            [In] D3D11Box* pDstBox,
            [In] void* pSrcData,
            [In] uint srcRowPitch,
            [In] uint srcDepthPitch
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT Map_(
            [In] IntPtr context,
            [In] IntPtr pResource,  // ID3D11Resource
            [In] uint subresource,
            [In] D3D11Map mapType,
            [In] uint mapFlags,
            [Out] out D3D11MappedSubresource pMappedResource
        );
        
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void Unmap_(
            [In] IntPtr context,
            [In] IntPtr pResource,  // ID3D11Resource
            [In] uint subresource
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT FinishCommandList_(
            [In] IntPtr context,
            [In, MarshalAs(UnmanagedType.Bool)] bool restoreDeferredContextState,
            [Out] out IntPtr ppCommandList
        );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void ExecuteCommandList_(
            [In] IntPtr context,
            [In] IntPtr pCommandList,
            [In, MarshalAs(UnmanagedType.Bool)] bool restoreContextState
        );
    }

    [StructLayout(LayoutKind.Sequential, Size = 24)]
    internal struct D3D11Box
    {
        // wont be using this one now
        // https://docs.microsoft.com/sv-se/windows/win32/api/d3d11/ns-d3d11-d3d11_box
    }
}
