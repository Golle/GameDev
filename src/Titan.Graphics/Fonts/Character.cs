using System.Numerics;
using Titan.Core.Math;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
    public readonly struct Character
    {
        public readonly Size Size;
        public readonly Vector2 Offset;
        public readonly int AdvanceX;
        public readonly TextureCoordinates TextureCoordinates;
        public Character(in Size size, in Vector2 offset, int advanceX, in TextureCoordinates textureCoordinates)
        {
            Size = size;
            Offset = offset;
            AdvanceX = advanceX;
            TextureCoordinates = textureCoordinates;
        }
    }
}
