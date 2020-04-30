using System;

namespace Titan.D3D11.Device.Models
{
    [Flags]
    public enum D3D11ResourceMiscFlag : long
    {
        GenerateMips = 0x1L,
        Shared = 0x2L,
        Texturecube = 0x4L,
        DrawindirectArgs = 0x10L,
        BufferAllowRawViews = 0x20L,
        BufferStructured = 0x40L,
        ResourceClamp = 0x80L,
        SharedKeyedmutex = 0x100L,
        GdiCompatible = 0x200L,
        SharedNthandle = 0x800L,
        RestrictedContent = 0x1000L,
        RestrictSharedResource = 0x2000L,
        RestrictSharedResourceDriver = 0x4000L,
        Guarded = 0x8000L,
        TilePool = 0x20000L,
        Tiled = 0x40000L,
        HwProtected = 0x80000L
    }
}
