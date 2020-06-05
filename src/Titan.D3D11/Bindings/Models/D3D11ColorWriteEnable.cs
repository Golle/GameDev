using System;

namespace Titan.D3D11.Bindings.Models
{
    [Flags]
    public enum D3D11ColorWriteEnable : byte
    {
        Red = 1,
        Green = 2,
        Blue = 4,
        Alpha = 8,
        All = (((Red | Green) | Blue) | Alpha)
    }
}
