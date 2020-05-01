using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11BufferDesc
    {
        public uint ByteWidth;
        public D3D11Usage Usage;
        public D3D11BindFlag BindFlags;
        public D3D11CpuAccessFlag CpuAccessFlags;
        public D3D11ResourceMiscFlag MiscFlags;
        public uint StructureByteStride;
    }
}
