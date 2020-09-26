using System;
using System.Collections.Generic;
using System.Linq;
using Titan.Tools.AssetsBuilder.Data;
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
            foreach (var group in obj.Groups)
            {
                if (string.IsNullOrWhiteSpace(@group.Name))
                {
                    throw new InvalidOperationException("Well this didn't work");
                }

                foreach (var subMesh in @group.Faces.GroupBy(f => f.Material))
                {
                    var vertices = new List<Vertex>();
                    var indices = new List<ushort>();

                    foreach (var face in subMesh)
                    {
                        if (face.Vertices.Length > 4)
                        {
                            throw new NotSupportedException("More than 4 vertices in a face is not supported");
                        }

                        foreach (var objVertex in face.Vertices.Take(3)) // triangulated face, just do a normal convertion
                        {
                            vertices.Add(CreateVertex(obj, objVertex));
                        }

                        if (face.Vertices.Length == 4)
                        {
                            vertices.Add(CreateVertex(obj, face.Vertices[0]));
                            vertices.Add(CreateVertex(obj, face.Vertices[2]));
                            vertices.Add(CreateVertex(obj, face.Vertices[3]));
                        }
                    }
                    yield return new Mesh(vertices.ToArray(), indices.ToArray());
                }
            }
        }

        private static Vertex CreateVertex(WavefrontObject obj, ObjVertex objVertex)
        {
            return new Vertex
            {
                Normal = obj.Normals[objVertex.NormalIndex],
                Position = obj.Positions[objVertex.VertexIndex],
                Texture = obj.Textures[objVertex.TextureIndex]
            };
        }
    }
}
