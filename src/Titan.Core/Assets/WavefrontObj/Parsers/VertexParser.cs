using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    internal class VertexParser : IVertexParser
    {
        public Vector3 Parse(string line)
        {
            var vertices = line
                .Trim()
                .Split(' ')
                .Select(v => float.Parse(v, CultureInfo.InvariantCulture))
                .ToArray();
            if (vertices.Length < 3)
            {
                throw new ParseException($"Invalid number of vertices: {vertices.Length}. Expected greater or equal to 3");
            }
            return new Vector3(vertices[0], vertices[1], vertices[2]);
        }
    }
}
