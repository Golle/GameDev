using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11BindFlag : ulong
    {
        VertexBuffer = 0x1L,
        IndexBuffer = 0x2L,
        ConstantBuffer = 0x4L,
        ShaderResource = 0x8L,
        StreamOutput = 0x10L,
        RenderTarget = 0x20L,
        DepthStencil = 0x40L,
        UnorderedAccess = 0x80L,
        Decoder = 0x200L,
        VideoEncoder = 0x400L
    }
}
