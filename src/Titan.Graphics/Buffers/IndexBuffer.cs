using System;
using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class IndexBuffer : IIndexBuffer
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11Buffer _buffer;
        private readonly short[] _indices;
        
        public ref readonly short[] Indicies => ref _indices; // should this me stored in memory? we can just access the buffer in the GPU

        public uint NumberOfIndices => (uint) _indices.Length;

        public IndexBuffer(ID3D11DeviceContext context, ID3D11Buffer buffer, in short[] indices)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            _indices = indices;
        }

        public IndexBuffer(ID3D11DeviceContext context, ID3D11Buffer buffer, uint size)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            _indices = new short[size];
        }

        public void Bind()
        {
            // TODO: offset and format should be configurable
            _context.SetIndexBuffer(_buffer, DxgiFormat.R16Uint, 0u);
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
