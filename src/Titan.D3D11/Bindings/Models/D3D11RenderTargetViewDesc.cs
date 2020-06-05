using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 20)] // Size = 20 is in 64 bit architecture, wont work in x86.
    internal struct D3D11RenderTargetViewDesc
    {
        // this struct has union, can use FieldOffsetAttribute to mimic that behavior. For now we'll leave it at null.
    }
}