using System;

namespace Titan.Graphics.Renderer.Passes
{
    // TODO: this should be made into a Resource type
    public interface IShaderResourceView
    {
        IntPtr NativeHandle { get; }
    }
}
