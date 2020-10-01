using System;
using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder.Converters
{
    internal class MeshBuilder
    {
        private readonly WavefrontObject _obj;

        private readonly ObjVertex[] _vertices = new ObjVertex[50000];
        private readonly ushort[] _indices = new ushort[50000];


        private int _indexCount = 0;
        private int _vertexCount = 0;
        public MeshBuilder(WavefrontObject obj)
        {
            _obj = obj;
        }

        public void AddVertex(in ObjVertex objVertex)
        {
            var vertexIndex = -1;
            for (var i = 0; i < _vertexCount; ++i)
            {
                ref var a = ref _vertices[i];
                if (a.NormalIndex == objVertex.NormalIndex && a.TextureIndex == objVertex.TextureIndex && objVertex.VertexIndex == a.VertexIndex)
                {
                    vertexIndex = i;
                    break;
                }
            }

            if (vertexIndex != -1)
            {
                _indices[_indexCount++] = (ushort) vertexIndex;
            }
            else
            {
                _indices[_indexCount++] = (ushort) _vertexCount;
                _vertices[_vertexCount++] = objVertex;
            }
        }

        public Mesh Build()
        {
            var vertices = new Vertex[_vertexCount];
            for (var i = 0; i < _vertexCount; ++i)
            {
                ref var vertex = ref _vertices[i];
                vertices[i] = new Vertex
                {
                    Position = _obj.Positions[vertex.VertexIndex]
                };
                if (vertex.TextureIndex != -1)
                {
                    vertices[i].Texture = _obj.Textures[vertex.TextureIndex];
                }
                if (vertex.NormalIndex != -1)
                {
                    vertices[i].Normal = _obj.Normals[vertex.NormalIndex];
                }
            }
            
            var mesh = new Mesh(vertices, new Span<ushort>(_indices, 0, _indexCount).ToArray());
            _vertexCount = 0;
            _indexCount = 0;
            return mesh;
        }
    }
}
