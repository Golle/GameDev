using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11Resource : IDisposable
    {
        IntPtr Handle { get; }
    }
}
