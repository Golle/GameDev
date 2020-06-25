using System.Collections.Generic;
using System.Linq;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    internal class FaceParser : IFaceParser
    {
        public Face[] Parse(string line)
        {
            static IEnumerable<Face> ParseFaces(string face)
            {
                var indices = face.Split('/').Select(f => int.TryParse((string?) f, out var index) ? index - 1 : -1).ToArray(); // Face Index starts with 1, subtract 1 to make it usable in .NET.
                yield return new Face(indices[0], indices[1], indices[2]);
            }
            return line
                .Trim()
                .Split(' ')
                .SelectMany(ParseFaces)
                .ToArray();
        }
    }
}