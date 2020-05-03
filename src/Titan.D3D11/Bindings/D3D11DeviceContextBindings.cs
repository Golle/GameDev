using System;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;

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
            [In] Color color
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextIASetVertexBuffers_(
            [In] IntPtr context,
            [In] uint startSlot,
            [In] uint numBuffers,
            [In] in IntPtr ppVertexbuffers,
            [In] in uint pStrides,
            [In] in uint pOffsets
        );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextDraw_(
            [In] IntPtr context,
            [In] uint vertexCount,
            [In] uint startLocation
        );


        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextVSSetShader_(
            [In] IntPtr context, 
            [In] IntPtr pVertexShader, 
            [In] in IntPtr ppClassInstances,
            [In] uint numClassInstances
            );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextPSSetShader_(
            [In] IntPtr context,
            [In] IntPtr pPixelShader,
            [In] in IntPtr ppClassInstances,
            [In] uint numClassInstances
        );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextRSSetViewports_(
            [In] IntPtr context,
            [In] uint numViewports,
            [In] D3D11Viewport[] viewports
        );


        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextIASetPrimitiveTopology_(
            [In] IntPtr context,
            [In] D3D11PrimitiveTopology topology
        );


        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DeviceContextIASetInputLayout_(
            [In] IntPtr context,
            [In] IntPtr inputLayout
        );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void IASetIndexBuffer_(
            [In] IntPtr handle,
            [In] IntPtr indexBuffer,
            [In] DxgiFormat format,
            [In] uint offset
        );

        [DllImport(Constants.D3D11Dll, SetLastError = true)]
        public static extern void DrawIndexed_(
            [In] IntPtr context,
            [In] uint indexCount,
            [In] uint startIndexLocation,
            [In] int baseVertexLocation
        );
    }
}
