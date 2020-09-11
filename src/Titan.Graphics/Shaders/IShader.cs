using System;

namespace Titan.Graphics.Shaders
{
    public interface IShader : IDisposable
    {
        IntPtr NativeHandle { get; }
    }
}
