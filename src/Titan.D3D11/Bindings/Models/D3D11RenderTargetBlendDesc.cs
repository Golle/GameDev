using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct D3D11RenderTargetBlendDesc
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool BlendEnable;
        public D3D11Blend SrcBlend;
        public D3D11Blend DestBlend;
        public D3D11BlendOp BlendOp;
        public D3D11Blend SrcBlendAlpha;
        public D3D11Blend DestBlendAlpha;
        public D3D11BlendOp BlendOpAlpha;
        public D3D11ColorWriteEnable RenderTargetWriteMask;
    }
}
