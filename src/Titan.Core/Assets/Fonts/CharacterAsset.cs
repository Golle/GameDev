using System.Numerics;
using Titan.Core.Math;

namespace Titan.Core.Assets.Fonts
{
    public struct CharacterAsset
    {
        public char Id { get; set; }
        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public Vector2 Offset { get; set; }
        public int AdvanceX { get; set; }
    }
}
