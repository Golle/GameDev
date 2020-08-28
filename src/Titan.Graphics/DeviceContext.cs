using System;
using System.Runtime.CompilerServices;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Graphics
{

    //internal class DeferredContext : IDeviceContext
    //{

    //}

    internal class DeviceContext : IDeviceContext
    {
        private readonly ID3D11DeviceContext _context;
        public DeviceContext(ID3D11DeviceContext context)
        {
            _context = context;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVertexBuffer(IVertexBuffer vertexBuffer, uint startSlot = 0) => _context.SetVertexBuffer(startSlot, vertexBuffer.NativeHandle, vertexBuffer.Strides, vertexBuffer.Offset);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIndexBuffer(IIndexBuffer indexBuffer, uint offset) => _context.SetIndexBuffer(indexBuffer.NativeHandle, DxgiFormat.R16Uint, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawIndexed(uint numberOfIndices, uint startIndexLocation, int baseVertexLocation) => _context.DrawIndexed(numberOfIndices, startIndexLocation, baseVertexLocation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixelShader(IPixelShader shader) => _context.SetPixelShader(shader.NativeHandle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInputLayout(IInputLayout inputLayout) => _context.SetInputLayout(inputLayout.NativeHandle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPrimitiveTopology(PrimitiveTopology topology) => _context.SetPrimitiveTopology((D3D11PrimitiveTopology)topology);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixelShaderConstantBuffer(IConstantBuffer constantBuffer, uint startSlot = 0) => _context.PSSetConstantBuffer(startSlot, constantBuffer.NativeHandle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVertexShaderConstantBuffer(IConstantBuffer constantBuffer, uint startSlot = 0) => _context.VSSetConstantBuffer(startSlot, constantBuffer.NativeHandle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVertexShader(IVertexShader shader) => _context.SetVertexShader(shader.NativeHandle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateConstantBuffer<T>(IConstantBuffer<T> constantBuffer, in T data) where T : unmanaged => _context.UpdateSubresourceData(constantBuffer.NativeHandle, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateVertexBuffer<T>(IVertexBuffer<T> vertexBuffer, in T[] data, int count) where T : unmanaged => _context.UpdateSubresourceData(vertexBuffer.NativeHandle, data);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateIndexBuffer(IIndexBuffer indexBuffer, in short[] data, int count) => _context.UpdateSubresourceData(indexBuffer.NativeHandle, data);
    }
}
