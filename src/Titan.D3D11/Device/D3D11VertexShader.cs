using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11VertexShader : ID3D11VertexShader
    {
        public IntPtr Handle { get; }
        public D3D11VertexShader(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
