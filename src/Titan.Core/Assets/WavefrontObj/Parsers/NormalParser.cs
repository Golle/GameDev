using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    internal class NormalParser : INormalParser
    {
        public Vector3 Parse(string line)
        {
            var normals = line
                .Trim()
                .Split(' ')
                .Select(v => float.Parse(v, CultureInfo.InvariantCulture))
                .ToArray();
            if (normals.Length < 3)
            {
                throw new ParseException($"Invalid number of normals: {normals.Length}. Expected greater or equal to 3");
            }
            return new Vector3(normals[0], normals[1], normals[2]);
        }
    }
}
