using System;

namespace Titan.Core.Assets.Angelfont.Model
{
    [Flags]
    public enum AngelfontChannel
    {
        // 1 = blue, 2 = green, 4 = red, 8 = alpha, 15 = all channels
        None = 0,
        Blue = 1,
        Green = 2,
        Red = 4,
        Alpha = 8,
        All = Blue|Green|Red|Alpha
    }
}
