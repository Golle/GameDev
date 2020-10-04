using System.IO;
using System.Numerics;
using Titan.Tools.AssetsBuilder.Converters;
using Titan.Tools.AssetsBuilder.Converters.Models;
using Titan.Tools.AssetsBuilder.Files;
using Titan.Tools.AssetsBuilder.Logging;

namespace Titan.Tools.AssetsBuilder.Data
{
    internal class ModelExporter : IModelExporter
    {
        private readonly IByteWriter _byteWriter;

        private readonly ILogger _logger;

        public ModelExporter(IByteWriter byteWriter, ILogger logger)
        {
            _byteWriter = byteWriter;
            _logger = logger;
        }

        public void Write(string filename, in Mesh mesh, bool overwrite)
        {
            using var file = File.OpenWrite(filename);
            file.SetLength(0);
            unsafe
            {
                var header = new Header
                {
                    VertexSize = sizeof(Vertex),
                    VertexCount = mesh.Vertices.Length,
                    IndexSize = sizeof(int),
                    IndexCount = mesh.Indices.Length,
                    SubMeshCount = mesh.SubMeshes.Length,
                };
                _logger.WriteLine($"Writing header to '{filename}' with mesh count {header.SubMeshCount}");
                _byteWriter.Write(file, header);
            }

            _logger.WriteLine($"Writing submesh {mesh.SubMeshes.Length}");
            _byteWriter.Write(file, mesh.SubMeshes);
            _logger.WriteLine($"Writing vertices {mesh.Vertices.Length}");
            _byteWriter.Write(file, mesh.Vertices);
            _logger.WriteLine($"Writing indices {mesh.Indices.Length}");
            _byteWriter.Write(file, mesh.Indices);
        }

        public void Write(string filename, in Mesh[] meshes, bool overwrite)
        {
            using var file = File.OpenWrite(filename);
            file.SetLength(0);
            unsafe
            {
                var header = new Header
                {
                    VertexSize = sizeof(Vertex),
                    SubMeshCount = meshes.Length,
                    IndexSize = sizeof(ushort)
                };
                _logger.WriteLine($"Writing header to '{filename}' with mesh count {meshes.Length}");
                _byteWriter.Write(file, header);
            }

            foreach (var mesh in meshes)
            {
                _logger.WriteLine("Writing mesh header");
                var (min, max) = GetBoundingBox(mesh);
                _byteWriter.Write(file, new MeshHeader
                {
                    IndexCount = mesh.Indices.Length,
                    VerticeCount = mesh.Vertices.Length,
                    Max = max,
                    Min = min
                });
                _logger.WriteLine($"Writing vertices {mesh.Vertices.Length}");
                _byteWriter.Write(file, mesh.Vertices);
                _logger.WriteLine($"Writing indices {mesh.Indices.Length}");
                _byteWriter.Write(file, mesh.Indices);
            }
        }

        private static (Vector3 min, Vector3 max) GetBoundingBox(in Mesh mesh)
        {
            var min = new Vector3(float.MaxValue);
            var max = new Vector3(float.MinValue);

            for (var i = 0; i < mesh.Vertices.Length; ++i)
            {
                ref var position = ref mesh.Vertices[i].Position;
                if (position.X < min.X) min.X = position.X;
                if (position.X > max.X) max.X = position.X;
                if (position.Y < min.Y) min.Y = position.Y;
                if (position.Y > max.Y) max.Y = position.Y;
                if (position.Z < min.Z) min.Z = position.Z;
                if (position.Z > max.Z) max.Z = position.Z;
            }

            return (min, max);
        }
    }
}
