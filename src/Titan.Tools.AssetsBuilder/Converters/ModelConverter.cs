using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder.Converters
{
    internal class ModelConverter : IModelConverter
    {
        private readonly ObjParser _objParser;
        public ModelConverter(ObjParser objParser)
        {
            _objParser = objParser;
        }

        public Mesh[] ConvertFromObj(string filename)
        {
            var obj = _objParser.ReadFromFile(filename).Result; // remove async api

            return CreateMeshes(obj).ToArray();
        }

        private static IEnumerable<Mesh> CreateMeshes(WavefrontObject obj)
        {
            var builder = new MeshBuilder(obj);
            foreach (var group in obj.Groups)
            {
                var timer = Stopwatch.StartNew();
                if (string.IsNullOrWhiteSpace(@group.Name))
                {
                    throw new InvalidOperationException("Well this didn't work");
                }

                var materialIndex = -1;
                foreach (var face in group.Faces)
                {
                    if (materialIndex == -1)
                    {
                        materialIndex = face.Material;
                    }

                    if (materialIndex != face.Material)
                    {
                        yield return builder.Build();
                        materialIndex = face.Material;
                    }

                    var vertices = face.Vertices;
                    if (vertices.Length > 4)
                    {
                        throw new NotSupportedException("More than 4 vertices in a face is not supported");
                    }

                    builder.AddVertex(vertices[0]);
                    builder.AddVertex(vertices[1]);
                    builder.AddVertex(vertices[2]);
                    if (face.Vertices.Length == 4
                    ) // 4 vertices per face, we need to triangulate the face to be able to use it in the engine.
                    {
                        builder.AddVertex(vertices[0]);
                        builder.AddVertex(vertices[2]);
                        builder.AddVertex(vertices[3]);
                    }
                }

                var meshes = builder.Build();
                yield return meshes;
                timer.Stop();
                Console.WriteLine($"Finished converting group {group.Name} in {timer.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}
