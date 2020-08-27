using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class VertexBuffer : IVertexBuffer
    {
        private readonly ID3D11Buffer _buffer;
        public uint NumberOfVertices { get; private set; }
        public uint Strides { get; }
        public uint Offset { get; }
        public IntPtr NativeHandle => _buffer.Handle;

        public VertexBuffer(ID3D11Buffer buffer, uint strides, uint length)
        {
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            Strides = strides;
            NumberOfVertices = length;
            Offset = 0; // TODO: offset needs to be configurable at some point
        }
        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
