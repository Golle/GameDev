using System;
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
                    }

                    if (element.Normal != -1)
                    {
                        vertex.Normals = model.Normals[element.Normal];
                    }

                    if (element.Texture != -1)
                    {
                        vertex.Texture = model.Textures[element.Texture];
                    }
                }
            }

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

            return new Mesh(_device.CreateVertexBuffer(vertexData), _device.CreateIndexBuffer(indices));
        }
    }
}
