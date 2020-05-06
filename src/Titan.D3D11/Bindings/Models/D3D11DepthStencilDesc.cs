using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11DepthStencilDesc
    {
        public bool DepthEnable;
        public D3D11DepthWriteMask DepthWriteMask;
        public D3D11ComparisonFunc DepthFunc;
        public bool StencilEnable;
        public sbyte StencilReadMask;
        public sbyte StencilWriteMask;
        public D3D11DepthStencilopDesc FrontFace;
        public D3D11DepthStencilopDesc BackFace;
    }
}