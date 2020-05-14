using System;

namespace Titan.Graphics.Buffers
{
    public interface IIndexBuffer : IDisposable
    {
        public uint NumberOfIndices { get; }
        public ref readonly short[] Indicies { get; }

        public void Bind();
    }
}
