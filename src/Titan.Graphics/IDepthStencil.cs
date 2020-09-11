using System;

namespace Titan.Graphics
{
    public interface IDepthStencil : IDisposable
    {
        IntPtr NativeHandle { get; }
    }
}
