using System;
using System.Collections.Generic;
using System.Numerics;
using Titan.Core.Assets;
using Titan.D3D11;

namespace Titan.Graphics.Models
{
    internal class MeshLoader : IMeshLoader
    {
        private readonly IDevice _device;
        private readonly IAssetLoader _assetLoader;

        public MeshLoader(IDevice device, IAssetLoader assetLoader)
        {
            _device = device;
            _assetLoader = assetLoader;
        }

        public IMesh Load(string filename)
        {
            var min = Vector3.Zero;
            var max = Vector3.Zero;

            var vertices = new HashSet<(Vector3, Vector2, Vector3)>();
            var vertices1 = new HashSet<(Vector3, Vector2)>();

            var model = _assetLoader.LoadModel(filename);
            
            var numberOfVertices = model.Faces.Length * 3;
            var vertexData = new TexturedVertex[numberOfVertices];
            var indices = new short[model.Faces.Length * 3];
            

            var vertexCount = 0;
            for (var i = 0; i < model.Faces.Length; ++i)
            {
                ref var face = ref model.Faces[i];
                for (var x = 0; x < face.Elements.Length; x++)
                {
                    indices[vertexCount] = (short) vertexCount;
                    ref var element = ref face.Elements[x];
                    ref var vertex = ref vertexData[vertexCount++];

                    vertex.Color = new Color(1f);
                    if (element.Vertex != -1)
                    {
                        vertex.Position = model.Vertices[element.Vertex];
                        UpdateBoundingBox(vertex.Position, ref min, ref max);
                    }

                    if (element.Normal != -1)
                    {
                        vertex.Normals = model.Normals[element.Normal];
                    }

                    if (element.Texture != -1)
                    {
                        vertex.Texture = model.Textures[element.Texture];
                    }

                    vertices.Add((vertex.Position, vertex.Texture, vertex.Normals));
                    vertices1.Add((vertex.Position, vertex.Texture));
                }
            }
            Console.WriteLine("{0} unique vertices(v3, v2, v3) out of {1}", vertices.Count, numberOfVertices);
            Console.WriteLine("{0} unique vertices(v3, v2) out of {1}", vertices1.Count, numberOfVertices);
            Console.WriteLine("Min: {0}  Max: {1}", min, max);

            //var vertexIndex = 0;
            //for (var i = 0; i < indices.Length; i += 6)
            //{
            //    indices[i] = (short)vertexIndex;
            //    indices[i + 1] = (short)(1 + vertexIndex);
            //    indices[i + 2] = (short)(2 + vertexIndex);
            //    indices[i + 3] = (short)(2 + vertexIndex);
            //    indices[i + 4] = (short)(3 + vertexIndex);
            //    indices[i + 5] = (short)(0 + vertexIndex);
            //    vertexIndex += 4;
            //}
            //vertexData = Model;
            //indices = Indices;

            return new Mesh(_device.CreateVertexBuffer(vertexData), _device.CreateIndexBuffer(indices), min, max);
        }

        private static void UpdateBoundingBox(in Vector3 position, ref Vector3 min, ref Vector3 max)
        {
            min.X = Math.Min(position.X, min.X);
            min.Y = Math.Min(position.Y, min.Y);
            min.Z = Math.Min(position.Z, min.Z);

            max.X = Math.Max(position.X, max.X);
            max.Y = Math.Max(position.Y, max.Y);
            max.Z = Math.Max(position.Z, max.Z);
        }
    }
}
