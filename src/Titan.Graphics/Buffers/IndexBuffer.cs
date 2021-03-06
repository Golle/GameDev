using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    // TODO: Make it possible to select u16 or u32 for indices
    internal class IndexBuffer : IIndexBuffer
    {
        private readonly ID3D11Buffer _buffer;
        private readonly int[] _indices;
        public IntPtr NativeHandle => _buffer.Handle;
        public ref readonly int[] Indicies => ref _indices; // should this me stored in memory? we can just access the buffer in the GPU
        public uint NumberOfIndices { get; private set; }

        public IndexBuffer(ID3D11Buffer buffer, in int[] indices)
        {
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            _indices = indices;
            NumberOfIndices = (uint) indices.Length;
        }

        public IndexBuffer(ID3D11Buffer buffer, uint size)
        {
            NumberOfIndices = size;
            _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }
        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
