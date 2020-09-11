using System;

namespace Titan.Graphics.Textures
{
    public interface ISampler : IDisposable
    {
        IntPtr NativeHandle { get; }
    }
}
