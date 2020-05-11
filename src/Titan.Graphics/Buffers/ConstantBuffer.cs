using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class ConstantBuffer<T> : IConstantBuffer<T>
    {
        private readonly ID3D11Buffer _buffer;

        public ConstantBuffer(ID3D11Buffer buffer)
        {
            _buffer = buffer;
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
