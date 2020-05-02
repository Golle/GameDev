using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11InputLayout : ID3D11InputLayout
    {
        public IntPtr Handle { get; }
        public D3D11InputLayout(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
