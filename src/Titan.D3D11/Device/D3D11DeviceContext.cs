using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;

namespace Titan.D3D11.Device
{
    internal class D3D11DeviceContext : ID3D11DeviceContext
    {
        private readonly IntPtr _handle;

        public D3D11DeviceContext(IntPtr handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(_handle);
        }

        public void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset)
        {
            D3D11DeviceContextBindings.DeviceContextIASetVertexBuffers_(_handle, startSlot, 1, vertexBuffer.Handle, strides, offset);
        }

        public void Draw(uint vertexCount, uint startLocation)
        {
            D3D11DeviceContextBindings.DeviceContextDraw_(_handle, vertexCount, startLocation);
        }

        public void SetRenderTargets(ID3D11RenderTargetView renderTarget)
        {
            D3D11DeviceContextBindings.DeviceContextOMSetRenderTargets_(_handle, 1, new[]{renderTarget.Handle}, IntPtr.Zero);
        }

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color)
        {
            D3D11DeviceContextBindings.ClearRenderTargetView(_handle, renderTarget.Handle, color);
        }

        public void SetVertexShader(ID3D11VertexShader vertexShader)
        {
            D3D11DeviceContextBindings.DeviceContextVSSetShader_(_handle, vertexShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetPixelShader(ID3D11PixelShader pixelShader)
        {
            D3D11DeviceContextBindings.DeviceContextPSSetShader_(_handle, pixelShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetViewport(in D3D11Viewport viewport)
        {
            D3D11DeviceContextBindings.DeviceContextRSSetViewports_(_handle, 1, new[] {viewport});
        }

        public void SetPrimitiveTopology(D3D11PrimitiveTopology topology)
        {
            D3D11DeviceContextBindings.DeviceContextIASetPrimitiveTopology_(_handle, topology);
        }

        public void SetInputLayout(ID3D11InputLayout inputLayout)
        {
            D3D11DeviceContextBindings.DeviceContextIASetInputLayout_(_handle, inputLayout.Handle);
        }
    }
}
