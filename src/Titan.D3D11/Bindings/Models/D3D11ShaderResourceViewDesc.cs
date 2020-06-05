using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{

    [StructLayout(LayoutKind.Explicit)]
    public struct D3D11ShaderResourceViewDesc
    {
        [FieldOffset(0)]
        public DxgiFormat Format;
        [FieldOffset(sizeof(DxgiFormat))]
        public D3D11SrvDimension ViewDimension;
        [FieldOffset(sizeof(DxgiFormat) + sizeof(D3D11SrvDimension))]
        
        //union {
        //    D3D11_BUFFER_SRV Buffer;
        //    D3D11_TEX1D_SRV Texture1D;
        //    D3D11_TEX1D_ARRAY_SRV Texture1DArray;
        public D3D11Tex2DSrv Texture2D; // Only support this one right now
        //    D3D11_TEX2D_ARRAY_SRV Texture2DArray;
        //    D3D11_TEX2DMS_SRV Texture2DMS;
        //    D3D11_TEX2DMS_ARRAY_SRV Texture2DMSArray;
        //    D3D11_TEX3D_SRV Texture3D;
        //    D3D11_TEXCUBE_SRV TextureCube;
        //    D3D11_TEXCUBE_ARRAY_SRV TextureCubeArray;
        //    D3D11_BUFFEREX_SRV BufferEx;
        //};
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct D3D11Tex2DSrv
    {
        public uint MostDetailedMip;
        public uint MipLevels;
    }
}
