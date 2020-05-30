using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11MappedSubresource
    {
        public unsafe void* pData;
        public uint RowPitch;
        public uint DepthPitch;
    }
}
