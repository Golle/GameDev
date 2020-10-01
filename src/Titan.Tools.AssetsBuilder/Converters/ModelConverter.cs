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


                    // TODO: add support for Concave faces (triangles done this way might overlap)

                    // 1st face => vertex 0, 1, 2
                    // 2nd face => vertex 0, 2, 3
                    // 3rd face => vertex 0, 4, 5
                    // 4th face => ...

                    builder.AddVertex(vertices[0]);
                    builder.AddVertex(vertices[1]);
                    builder.AddVertex(vertices[2]);

                    // more than 3 vertices per face, we need to triangulate the face to be able to use it in the engine.
                    
                    for (var i = 3; i < vertices.Length; ++i)
                    {
                        builder.AddVertex(vertices[0]);
                        builder.AddVertex(vertices[i-1]);
                        builder.AddVertex(vertices[i]);
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
