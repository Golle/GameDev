using System;
using System.Runtime.InteropServices;

namespace Titan.Xaudio2
{
    public class ComPointer : IDisposable
    {
        public IntPtr Handle { get; }

        public ComPointer(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            Marshal.Release(Handle);
        }
    }
}
