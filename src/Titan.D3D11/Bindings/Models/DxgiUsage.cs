namespace Titan.D3D11.Bindings.Models
{
    //https://docs.microsoft.com/en-us/windows/win32/direct3ddxgi/dxgi-usage
    internal enum DxgiUsage : uint
    {
        ShaderInput = 0x00000010U,
        RenderTargetOutput = 0x00000020,
        BackBuffer = 0x00000040U,
        Shared = 0x00000080U,
        ReadOnly = 0x00000100U,
        DiscardOnPresent = 0x00000200U,
        UnorderedAccess = 0x00000400U,
    }
}
