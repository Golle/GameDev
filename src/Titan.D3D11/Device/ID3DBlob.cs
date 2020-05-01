using System;

namespace Titan.D3D11.Device
{
    public interface ID3DBlob : IDisposable
    {
        UIntPtr GetBufferSize();
        IntPtr GetBufferPointer();
    }
}
