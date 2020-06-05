using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct D3D11SamplerDesc
    {
        public D3D11Filter Filter;
        public D3D11TextureAddressMode AddressU;
        public D3D11TextureAddressMode AddressV;
        public D3D11TextureAddressMode AddressW;
        public float MipLODBias;
        public uint MaxAnisotropy;
        public D3D11ComparisonFunc ComparisionFunc;
        public Color BorderColor;
        public float MinLOD;
        public float MaxLOD;
    }
}
