using System;

namespace Titan.Tools.FontGenerator.Angelfont.Model
{
    [Flags]
    public enum Channel
    {
        // 1 = blue, 2 = green, 4 = red, 8 = alpha, 15 = all channels
        Blue = 1,
        Green = 2,
        Red = 4,
        Alpha = 8,
        All = Blue|Green|Red|Alpha
    }
}
