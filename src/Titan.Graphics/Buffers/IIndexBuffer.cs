using System;

namespace Titan.Graphics.Buffers
{
    public interface IIndexBuffer : IResource, IDisposable
    {
        public uint NumberOfIndices { get; }
        public ref readonly short[] Indicies { get; }
    }
}
