using System;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
    public interface IFont : IDisposable
    {
        ITexture2D Texture { get; }
        public int LineHeight { get; }
        public int Baseline { get; }
        public int FontSize { get; }
        public Character GetCharacter(char c);
    }
}
