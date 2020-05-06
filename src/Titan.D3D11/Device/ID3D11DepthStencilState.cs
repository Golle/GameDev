using System;

namespace Titan.D3D11.Device
{
    public interface ID3D11DepthStencilState : IDisposable
    {
        IntPtr Handle { get; }
    }
}
