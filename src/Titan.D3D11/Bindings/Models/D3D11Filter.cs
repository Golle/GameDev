namespace Titan.D3D11.Bindings.Models
{
    public enum D3D11Filter : uint
    {
        MinMagMipPoint = 0,
        MinMagPointMipLinear = 0x1,
        MinPointMagLinearMipPoint = 0x4,
        MinPointMagMipLinear = 0x5,
        MinLinearMagMipPoint = 0x10,
        MinLinearMagPointMipLinear = 0x11,
        MinMagLinearMipPoint = 0x14,
        MinMagMipLinear = 0x15,
        Anisotropic = 0x55,
        ComparisonMinMagMipPoint = 0x80,
        ComparisonMinMagPointMipLinear = 0x81,
        ComparisonMinPointMagLinearMipPoint = 0x84,
        ComparisonMinPointMagMipLinear = 0x85,
        ComparisonMinLinearMagMipPoint = 0x90,
        ComparisonMinLinearMagPointMipLinear = 0x91,
        ComparisonMinMagLinearMipPoint = 0x94,
        ComparisonMinMagMipLinear = 0x95,
        ComparisonAnisotropic = 0xd5,
        MinimumMinMagMipPoint = 0x100,
        MinimumMinMagPointMipLinear = 0x101,
        MinimumMinPointMagLinearMipPoint = 0x104,
        MinimumMinPointMagMipLinear = 0x105,
        MinimumMinLinearMagMipPoint = 0x110,
        MinimumMinLinearMagPointMipLinear = 0x111,
        MinimumMinMagLinearMipPoint = 0x114,
        MinimumMinMagMipLinear = 0x115,
        MinimumAnisotropic = 0x155,
        MaximumMinMagMipPoint = 0x180,
        MaximumMinMagPointMipLinear = 0x181,
        MaximumMinPointMagLinearMipPoint = 0x184,
        MaximumMinPointMagMipLinear = 0x185,
        MaximumMinLinearMagMipPoint = 0x190,
        MaximumMinLinearMagPointMipLinear = 0x191,
        MaximumMinMagLinearMipPoint = 0x194,
        MaximumMinMagMipLinear = 0x195,
        MaximumAnisotropic = 0x1d5
    }
}
