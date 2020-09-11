using System;

namespace Titan.Graphics.Buffers
{
    public interface IResource
    {
        IntPtr NativeHandle { get; }
    }
}
