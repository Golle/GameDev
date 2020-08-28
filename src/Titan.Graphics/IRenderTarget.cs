using System;

namespace Titan.Graphics
{
    public interface IRenderTarget : IDisposable
    {
        IntPtr NativeHandle { get; }
    }
}
