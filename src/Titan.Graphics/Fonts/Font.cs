using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
    internal class Font : IFont
    {
        public ITexture2D Texture { get; }
        public int LineHeight { get; }
        public int Baseline { get; }
        private readonly IDictionary<char, Character> _characters;
        private readonly Character _fallbackCharacter;
        public Font(int baseline, int lineHeight, ITexture2D texture, IDictionary<char, Character> characters, in Character fallbackCharacter)
        {
            _characters = characters;
            _fallbackCharacter = fallbackCharacter;
            Baseline = baseline;
            LineHeight = lineHeight;
            Texture = texture;
        }

        // TODO: Add support for Fallback character
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Character GetCharacter(char c) => _characters.TryGetValue(c, out var character) ? character : _fallbackCharacter;

        public void Dispose() => Texture.Dispose();
    }
}
