using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Buffers
{
    internal class ConstantBuffer<T> : IConstantBuffer<T> where T : unmanaged
    {
        private readonly ID3D11Buffer _buffer;
        public IntPtr NativeHandle => _buffer.Handle;
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
