using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.Common;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Models
{
    internal class ModelLoader : IMeshLoader
    {
        private readonly IByteReader _byteReader;
        private readonly IDevice _device;

        public ModelLoader(IByteReader byteReader, IDevice device)
        {
            _byteReader = byteReader;
            _device = device;
        }

        public IMesh Load(string filename)
        {
            using var file = File.OpenRead(filename);

            _byteReader.Read<Header>(file, out var header);

            var submeshes = new SubMesh[header.SubMeshCount];
            _byteReader.Read(file, ref submeshes);

            IVertexBuffer<TexturedVertex> vertexBuffer;
            {
                var buffer = Marshal.AllocHGlobal(header.VertexSize * header.VertexCount);
                unsafe
                {
                    var bufferPointer = buffer.ToPointer();
                    _byteReader.Read<TexturedVertex>(file, bufferPointer, header.VertexCount);
                    vertexBuffer = _device.CreateVertexBuffer<TexturedVertex>(bufferPointer, header.VertexCount);
                }
                Marshal.FreeHGlobal(buffer);
            }

            IIndexBuffer indexBuffer;
            {
                var buffer = Marshal.AllocHGlobal(header.IndexSize * header.IndexCount);
                unsafe
                {
                    var bufferPointer = buffer.ToPointer();
                    _byteReader.Read<int>(file, bufferPointer, header.IndexCount);
                    indexBuffer = _device.CreateIndexBuffer(bufferPointer, header.IndexCount);
                }
                Marshal.FreeHGlobal(buffer);
            }
            return new Mesh(vertexBuffer, indexBuffer, submeshes);



            //for (var i = 0; i < header.MeshCount; ++i)
            //{
            //    _byteReader.Read<MeshHeader>(file, out var meshHeader);
            //    var memory = Marshal.AllocHGlobal(header.VertexSize * meshHeader.VerticesCount);
            //    IVertexBuffer<TexturedVertex> vertexBuffer;
            //    unsafe
            //    {
            //        _byteReader.Read<TexturedVertex>(file, memory.ToPointer(), meshHeader.VerticesCount);
            //        vertexBuffer = _device.CreateVertexBuffer<TexturedVertex>(memory.ToPointer(), meshHeader.VerticesCount);
            //    }
            //    Marshal.FreeHGlobal(memory);
            //    //var vertexBuffer = _device.CreateVertexBuffer<TexturedVertex>((uint)meshHeader.VerticesCount, BufferUsage.Dynamic, BufferAccessFlags.Write);
            //    //unsafe
            //    //{
            //    //    _device.ImmediateContext.Map(vertexBuffer, out var ptr);
            //    //    _byteReader.Read<TexturedVertex>(file, ptr, meshHeader.VerticesCount);
            //    //}
            //    //_device.ImmediateContext.Unmap(vertexBuffer);

            //    var indexBuffer = _device.CreateIndexBuffer((uint) meshHeader.IndexCount, BufferUsage.Dynamic, BufferAccessFlags.Write);
            //    unsafe
            //    {
            //        _device.ImmediateContext.Map(indexBuffer, out var ptr);
            //        _byteReader.Read<ushort>(file, ptr, meshHeader.IndexCount);
            //    }
            //    _device.ImmediateContext.Unmap(indexBuffer);
            //    meshes[i] = new Mesh(vertexBuffer, indexBuffer, meshHeader.Min, meshHeader.Max);
            //}
        }
    }
    
    [StructLayout(LayoutKind.Sequential, Size = 256)]
    internal struct Header
    {
        public ushort Version;
        public int VertexSize;
        public int VertexCount;
        public int IndexSize;
        public int IndexCount;
        public int SubMeshCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SubMesh
    {
        public int StartIndex;
        public int Count;
        public int MaterialIndex;
        public Vector3 Min;
        public Vector3 Max;
    }
}
