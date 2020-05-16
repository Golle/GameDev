using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class VertexBuffer<T> : IVertexBuffer<T> where T: unmanaged
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11Buffer _buffer;
        private readonly uint _strides;

        public VertexBuffer(ID3D11DeviceContext context, ID3D11Buffer buffer, uint strides)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            _strides = strides;
        }

        public void Bind()
        {
            // TODO: offset needs to be configurable at some point
            _context.SetVertexBuffer(0, _buffer, _strides, 0); 
        }

        public void SetData(in T[] vertices, int numberOfVertices)
        {
            unsafe
            {
                fixed (void* pointer = vertices)
                {
                    _context.UpdateSubresourceData(_buffer, pointer);
                }
            }
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
