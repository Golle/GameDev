using System;

namespace Titan.Graphics.Stuff
{
    public interface IDevice : IDisposable
    {
        IIndexBuffer CreateIndexBuffer();
        IVertexBuffer<T> CreateVertexBuffer<T>() where T : unmanaged, IVertex;
        IConstantBuffer CrateConstantBuffer();


        void BeginRender();
        void EndRender();
    }
}
