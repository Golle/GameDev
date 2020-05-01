using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11ResourceMiscFlag : uint
    {
        Unspecified = 0,
        GenerateMips = 0x1,
        Shared = 0x2,
        Texturecube = 0x4,
        DrawindirectArgs = 0x10,
        BufferAllowRawViews = 0x20,
        BufferStructured = 0x40,
        ResourceClamp = 0x80,
        SharedKeyedmutex = 0x100,
        GdiCompatible = 0x200,
        SharedNthandle = 0x800,
        RestrictedContent = 0x1000,
        RestrictSharedResource = 0x2000,
        RestrictSharedResourceDriver = 0x4000,
        Guarded = 0x8000,
        TilePool = 0x20000,
        Tiled = 0x40000,
        HwProtected = 0x80000
    }
}
