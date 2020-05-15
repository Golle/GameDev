using Titan.D3D11.Bindings.Models;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class ConstantBuffer<T> : IConstantBuffer<T> where T : unmanaged
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11Buffer _buffer;

        public ConstantBuffer(ID3D11DeviceContext context, ID3D11Buffer buffer)
        {
            _context = context;
            _buffer = buffer;
        }

        public void BindToPixelShader()
        {
            _context.PSSetConstantBuffer(0, _buffer);
        }
        public void BindToVertexShader()
        {
            _context.VSSetConstantBuffer(0, _buffer);
        }

        public void Update(in T data)
        {
            unsafe
            {
                fixed (void* dataPointer = &data)
                {
                    D3D11SubresourceData subresourceData = default;
                    subresourceData.pSysMem = dataPointer;
                    _context.UpdateSubresourceData(_buffer, subresourceData);
                }
            }
        }


        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
