using System;

namespace Titan.Graphics.Layout
{
    public interface IInputLayout : IDisposable
    {
        IntPtr NativeHandle { get; }
    }
}
