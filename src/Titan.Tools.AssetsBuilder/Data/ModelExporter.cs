using System.IO;
using Titan.Tools.AssetsBuilder.Converters;
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

        public void Write(string filename, in Mesh[] meshes, bool overwrite)
        {
            using var file = File.OpenWrite(filename);
            file.SetLength(0);
            unsafe
            {
                var header = new Header
                {
                    VertexSize = sizeof(Vertex),
                    MeshCount = meshes.Length,
                    IndexSize = sizeof(ushort)
                };
                _logger.WriteLine($"Writing header to '{filename}' with mesh count {meshes.Length}");
                _byteWriter.Write(file, header);
            }

            foreach (var mesh in meshes)
            {
                _logger.WriteLine("Writing mesh header");
                _byteWriter.Write(file, new MeshHeader
                {
                    IndexCount = mesh.Indices.Length,
                    VerticeCount = mesh.Vertices.Length
                });
                _logger.WriteLine($"Writing vertices {mesh.Vertices.Length}");
                _byteWriter.Write(file, mesh.Vertices);
                _logger.WriteLine($"Writing indices {mesh.Indices.Length}");
                _byteWriter.Write(file, mesh.Indices);
            }
        }
    }
}
