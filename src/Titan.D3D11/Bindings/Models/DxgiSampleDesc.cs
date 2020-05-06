using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DxgiSampleDesc
    {
        public uint Count;
        public uint Quality;
    }
}
