using System;

namespace Titan.Graphics.Buffers
{
    public interface IConstantBuffer<T> : IDisposable where T: unmanaged
    {
        
        void BindToPixelShader(uint startSlot = 0);
        void BindToVertexShader(uint startSlot = 0);
        void Update(in T data);
    }
}
