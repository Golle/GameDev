using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    public interface ITextureParser
    {
        Vector2 Parse(string line);
    }
}
