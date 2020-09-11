using System;

namespace Titan.Graphics.Buffers
{
    public interface IConstantBuffer : IResource, IDisposable
    {
    }

    public interface IConstantBuffer<T> : IConstantBuffer where T: unmanaged
    {
    }
}
