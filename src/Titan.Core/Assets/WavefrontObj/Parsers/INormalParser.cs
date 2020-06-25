using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    public interface INormalParser
    {
        Vector3 Parse(string line);
    }
}
