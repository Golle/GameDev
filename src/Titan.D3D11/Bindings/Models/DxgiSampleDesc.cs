using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DxgiSampleDesc
    {
        public uint Count;
        public uint Quality;
    }
}
