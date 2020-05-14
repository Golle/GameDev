using System;

namespace Titan.Graphics.Blobs
{
    public interface IBlob : IDisposable
    {
        UIntPtr Size { get; }
        IntPtr Buffer { get; }
    }
}
