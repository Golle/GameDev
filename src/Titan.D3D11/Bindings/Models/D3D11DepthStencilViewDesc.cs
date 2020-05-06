using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Explicit)]
    public struct D3D11DepthStencilViewDesc
    {
        [FieldOffset(0)]
        public DxgiFormat Format;
        [FieldOffset(4)]
        public D3D11DsvDimension ViewDimension;
        [FieldOffset(8)]
        public uint Flags;

        // Enable these when/if needed
        //[FieldOffset(12)]
        //    D3D11_TEX1D_DSV Texture1D;
        //[FieldOffset(12)]
        //    D3D11_TEX1D_ARRAY_DSV Texture1DArray;
        [FieldOffset(12)]
        public D3D11Tex2DDsv Texture2D;
        //[FieldOffset(12)]
        //    D3D11_TEX2D_ARRAY_DSV Texture2DArray;
        //[FieldOffset(12)]
        //    D3D11_TEX2DMS_DSV Texture2DMS;
        //[FieldOffset(12)]
        //    D3D11_TEX2DMS_ARRAY_DSV Texture2DMSArray;
    }
}