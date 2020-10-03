using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 20)] // Size = 20 is in 64 bit architecture, wont work in x86.
    internal struct D3D11RenderTargetViewDesc
    {
        [FieldOffset(0)]
        public DxgiFormat Format;
        [FieldOffset(4)]
        public D3D11RtvDimension ViewDimension;
        [FieldOffset(8)]
        //union 
        //{
        //    D3D11_BUFFER_RTV Buffer;
        //    D3D11_TEX1D_RTV Texture1D;
        //    D3D11_TEX1D_ARRAY_RTV Txture1DArray;
        public D3D11Tex2DRtv Texture2D;
        //    D3D11_TEX2D_ARRAY_RTV Texture2DArray;
        //    D3D11_TEX2DMS_RTV Texture2DMS;
        //    D3D11_TEX2DMS_ARRAY_RTV Texture2DMSArray;
        //    D3D11_TEX3D_RTV Texture3D;
        //};
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct D3D11Tex2DRtv
    {
        public uint MipSlice;
    }

    internal enum D3D11RtvDimension
    {
        Unknown = 0,
        Buffer = 1,
        Texture1D = 2,
        Texture1Darray = 3,
        Texture2D = 4,
        Texture2Darray = 5,
        Texture2Dms = 6,
        Texture2Dmsarray = 7,
        Texture3D = 8
    }
}


