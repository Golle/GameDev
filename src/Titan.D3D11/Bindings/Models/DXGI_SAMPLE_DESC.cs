using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DXGI_SAMPLE_DESC
    {
        public uint Count;
        public uint Quality;
    }
}