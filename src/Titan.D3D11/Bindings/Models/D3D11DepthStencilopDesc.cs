using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11DepthStencilopDesc
    {
        public D3D11StencilOp StencilFailOp;
        public D3D11StencilOp StencilDepthFailOp;
        public D3D11StencilOp StencilPassOp;
        public D3D11ComparisonFunc StencilFunc;
    }
}