using System;
using System.Collections.Generic;
using System.Linq;

namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    internal class FaceParser : IFaceParser
    {
        public FaceElement[] Parse(string line)
        {
            static IEnumerable<FaceElement> ParseFaces(string face)
            {
                var indices = face.Split('/').Select(f => int.TryParse(f, out var index) ? index - 1 : -1).ToArray(); // Face Index starts with 1, subtract 1 to make it usable in .NET.
                yield return new FaceElement(indices[0], indices[1], indices[2]);
            }

            var faces = line
                .Trim()
                .Split(' ')
                .ToArray();

            if (faces.Length != 3)
            {
                throw new NotSupportedException($"Faces must be triangulated. Number of faces found: {faces.Length}");
            }

            return faces
                .SelectMany(ParseFaces)
                .ToArray();
        }
    }
}
