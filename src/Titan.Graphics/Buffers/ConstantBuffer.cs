using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class ConstantBuffer<T> : IConstantBuffer<T> where T : unmanaged
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11Buffer _buffer;
        public IntPtr NativeHandle => _buffer.Handle;
        public ConstantBuffer(ID3D11DeviceContext context, ID3D11Buffer buffer)
        {
            _context = context;
            _buffer = buffer;
        }

        public void BindToPixelShader(uint startSlot = 0)
        {
            _context.PSSetConstantBuffer(startSlot, _buffer);
        }

        public void BindToVertexShader(uint startSlot = 0)
        {
            _context.VSSetConstantBuffer(startSlot, _buffer);
        }

        public void Update(in T data)
        {
            unsafe
            {
                fixed (void* dataPointer = &data)
                {
                    _context.UpdateSubresourceData(_buffer, dataPointer);
                }
            }
        }


        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
