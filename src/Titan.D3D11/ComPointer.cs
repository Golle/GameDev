using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11
{
    internal class ComPointer : IDisposable
    {
        public IntPtr Handle { get; }
        
        public ComPointer(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
