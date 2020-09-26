using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.Common;

namespace Titan.Graphics.Models
{
    internal interface IModelLoader
    {
        IMesh[] LoadModel(string filename);
    }

    internal class ModelLoader : IMeshLoader
    {
        private readonly IByteReader _byteReader;
        private readonly IDevice _device;

        public ModelLoader(IByteReader byteReader, IDevice device)
        {
            _byteReader = byteReader;
            _device = device;
        }

        public IMesh[] Load(string filename)
        {
            using var file = File.OpenRead(filename);

            _byteReader.Read<Header>(file, out var header);

            var meshes = new IMesh[header.MeshCount];
            for (var i = 0; i < header.MeshCount; ++i)
            {
                _byteReader.Read<MeshHeader>(file, out var meshHeader);

                var vertexBuffer = _device.CreateVertexBuffer<TexturedVertex>((uint) meshHeader.VerticesCount, BufferUsage.Dynamic,BufferAccessFlags.Write);
                unsafe
                {
                    _device.ImmediateContext.Map(vertexBuffer, out var ptr);
                    _byteReader.Read<TexturedVertex>(file, ptr, meshHeader.VerticesCount);
                }
                _device.ImmediateContext.Unmap(vertexBuffer);

                var indexBuffer = _device.CreateIndexBuffer((uint) meshHeader.IndexCount, BufferUsage.Dynamic, BufferAccessFlags.Write);
                unsafe
                {
                    _device.ImmediateContext.Map(indexBuffer, out var ptr);
                    _byteReader.Read<ushort>(file, ptr, meshHeader.IndexCount);
                }
                _device.ImmediateContext.Unmap(indexBuffer);
                meshes[i] = new Mesh(vertexBuffer, indexBuffer, meshHeader.Min, meshHeader.Max);
            }
            return meshes;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 256)]
    internal struct Header
    {
        public ushort Version;
        public int VertexSize;
        public int IndexSize;
        public int MeshCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MeshHeader
    {
        public Vector3 Min;
        public Vector3 Max;
        public int VerticesCount;
        public int IndexCount;
    }
}
