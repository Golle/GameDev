using System;
using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder.Converters
{
    internal class MeshBuilder
    {
        private readonly WavefrontObject _obj;

        private readonly ObjVertex[] _vertices = new ObjVertex[200_000];
        private readonly int[] _indices = new int[800_000];
        private readonly SubMesh[] _meshes = new SubMesh[10000];
        
        private int _indexCount = 0;
        private int _vertexCount = 0;
        private int _submeshCount = 0;
        
        private int _currentMaterial = -1;

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
                _indices[_indexCount++] = vertexIndex;
            }
            else
            {
                _indices[_indexCount++] = _vertexCount;
                _vertices[_vertexCount++] = objVertex;
            }
        }

        public void SetMaterial(in int material)
        {
            if (_currentMaterial == material)
            {
                return;
            }
            _currentMaterial = material;
            SetCountForCurrentMesh();
            ref var mesh = ref _meshes[_submeshCount++];
            mesh.StartIndex = _indexCount;
            mesh.MaterialIndex = _currentMaterial;
        }

        private void SetCountForCurrentMesh()
        {
            if (_submeshCount > 0)
            {
                ref var prevMesh = ref _meshes[_submeshCount - 1];
                prevMesh.Count = _indexCount - prevMesh.StartIndex;
            }
        }

        public Mesh Build()
        {
            SetCountForCurrentMesh();

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
            return new Mesh(vertices, new Span<int>(_indices, 0, _indexCount).ToArray(), new Span<SubMesh>(_meshes, 0, _submeshCount).ToArray());
        }
    }
}
