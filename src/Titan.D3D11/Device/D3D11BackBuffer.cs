using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11BackBuffer : ID3D11BackBuffer
    {
        public IntPtr Handle { get; }
        public D3D11BackBuffer(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
