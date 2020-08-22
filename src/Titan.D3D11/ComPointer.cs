using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11
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
            //D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
