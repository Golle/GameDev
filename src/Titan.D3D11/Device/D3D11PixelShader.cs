using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    internal class D3D11PixelShader : ID3D11PixelShader
    {
        public IntPtr Handle { get; }
        public D3D11PixelShader(IntPtr handle)
        {
            Handle = handle;
        }
        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
