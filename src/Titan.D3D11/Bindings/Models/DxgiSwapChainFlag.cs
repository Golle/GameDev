namespace Titan.D3D11.Bindings.Models
{
    internal enum DxgiSwapChainFlag : uint
    {
        Nonprerotated = 1,
        AllowModeSwitch = 2,
        GdiCompatible = 4,
        RestrictedContent = 8,
        RestrictSharedResourceDriver = 16,
        DisplayOnly = 32,
        FrameLatencyWaitableObject = 64,
        ForegroundLayer = 128,
        FullscreenVideo = 256,
        YuvVideo = 512,
        HwProtected = 1024,
        AllowTearing = 2048,
        RestrictedToAllHolographicDisplays = 4096
    }
}
