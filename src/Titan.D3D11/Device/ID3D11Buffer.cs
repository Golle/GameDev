using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.Device
{
    public interface ID3D11Buffer : IDisposable
    {
        IntPtr Handle { get; }
    }

    internal class D3D11Buffer : ID3D11Buffer
    {
        public IntPtr Handle { get; }

        public D3D11Buffer(IntPtr handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(Handle);
        }
    }
}
