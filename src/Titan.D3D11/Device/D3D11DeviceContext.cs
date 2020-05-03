using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Bindings.Models;

namespace Titan.D3D11.Device
{
    internal class D3D11DeviceContext : ComPointer, ID3D11DeviceContext
    {
        public D3D11DeviceContext(IntPtr handle) 
            : base(handle)
        {
        }

        public void SetVertexBuffer(uint startSlot, ID3D11Buffer vertexBuffer, uint strides, uint offset)
        {
            D3D11DeviceContextBindings.DeviceContextIASetVertexBuffers_(Handle, startSlot, 1, vertexBuffer.Handle, strides, offset);
        }

        public void Draw(uint vertexCount, uint startLocation)
        {
            D3D11DeviceContextBindings.DeviceContextDraw_(Handle, vertexCount, startLocation);
        }

        public void SetRenderTargets(ID3D11RenderTargetView renderTarget)
        {
            D3D11DeviceContextBindings.DeviceContextOMSetRenderTargets_(Handle, 1, new[]{renderTarget.Handle}, IntPtr.Zero);
        }

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTarget, in Color color)
        {
            D3D11DeviceContextBindings.ClearRenderTargetView(Handle, renderTarget.Handle, color);
        }

        public void SetVertexShader(ID3D11VertexShader vertexShader)
        {
            D3D11DeviceContextBindings.DeviceContextVSSetShader_(Handle, vertexShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetPixelShader(ID3D11PixelShader pixelShader)
        {
            D3D11DeviceContextBindings.DeviceContextPSSetShader_(Handle, pixelShader.Handle, IntPtr.Zero, 0u);
        }

        public void SetViewport(in D3D11Viewport viewport)
        {
            D3D11DeviceContextBindings.DeviceContextRSSetViewports_(Handle, 1, new[] {viewport});
        }

        public void SetPrimitiveTopology(D3D11PrimitiveTopology topology)
        {
            D3D11DeviceContextBindings.DeviceContextIASetPrimitiveTopology_(Handle, topology);
        }

        public void SetInputLayout(ID3D11InputLayout inputLayout)
        {
            D3D11DeviceContextBindings.DeviceContextIASetInputLayout_(Handle, inputLayout.Handle);
        }

        public void SetIndexBuffer(ID3D11Buffer buffer, DxgiFormat format, uint offset)
        {
            D3D11DeviceContextBindings.IASetIndexBuffer_(Handle, buffer.Handle, format, offset);
        }

        public void DrawIndexed(uint indexCount, uint startIndexLocation, int baseVertexLocation)
        {
            D3D11DeviceContextBindings.DrawIndexed_(Handle, indexCount, startIndexLocation, baseVertexLocation);
        }
    }
}
