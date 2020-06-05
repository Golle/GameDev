using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11BlendDesc
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool AlphaToCoverageEnable;
        [MarshalAs(UnmanagedType.Bool)]
        public bool IndependentBlendEnable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public D3D11RenderTargetBlendDesc[] RenderTargets;
        public static D3D11BlendDesc Default
        {
            get
            {
                var desc = new D3D11BlendDesc {RenderTargets = new D3D11RenderTargetBlendDesc[8]};
                for (var i = 0; i < desc.RenderTargets.Length; ++i)
                {
                    ref var renderTarget = ref desc.RenderTargets[i];
                    renderTarget.BlendEnable = false;
                    renderTarget.SrcBlend = D3D11Blend.One;
                    renderTarget.DestBlend = D3D11Blend.Zero;
                    renderTarget.BlendOp = D3D11BlendOp.Add;
                    renderTarget.SrcBlendAlpha = D3D11Blend.One;
                    renderTarget.DestBlendAlpha = D3D11Blend.Zero;
                    renderTarget.BlendOpAlpha = D3D11BlendOp.Add;
                    renderTarget.RenderTargetWriteMask = D3D11ColorWriteEnable.All;
                }
                return desc;
            }
        }
    }
}
