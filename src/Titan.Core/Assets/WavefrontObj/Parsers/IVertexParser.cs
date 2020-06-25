using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    public interface IVertexParser
    {
        Vector3 Parse(string line);
    }
}
