using Titan.D3D11.Device;
using Titan.Graphics.Stuff;

namespace Titan.Graphics.Buffers
{
    internal class VertexBuffer<T> : IVertexBuffer<T> where T: unmanaged, IVertex
    {
        private readonly ID3D11Buffer _buffer;
        public VertexBuffer(ID3D11Buffer buffer)
        {
            _buffer = buffer;
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
