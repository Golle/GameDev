using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class VertexBuffer<T> : IVertexBuffer<T> where T: unmanaged
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
