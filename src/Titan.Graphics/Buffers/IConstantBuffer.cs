using System;

namespace Titan.Graphics.Buffers
{
    public interface IConstantBuffer<T> : IDisposable where T: unmanaged
    {
        
        void BindToPixelShader();
        void BindToVertexShader();
        void Update(in T data);
    }
}
