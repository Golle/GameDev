using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    internal class TextureParser : ITextureParser
    {
        public Vector2 Parse(string line)
        {
            var textures = line
                .Trim()
                .Split(' ')
                .Select(v => float.Parse(v, CultureInfo.InvariantCulture))
                .ToArray();
            if (textures.Length != 2 && textures.Length != 3)
            {
                throw new ParseException($"Invalid number of texture coordinates: {textures.Length}. Expected 2 or 3");
            }
            return new Vector2(textures[0], textures[1]);
        }
    }
}
