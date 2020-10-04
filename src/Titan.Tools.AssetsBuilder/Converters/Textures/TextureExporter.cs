using System.IO;
using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.Files;
using Titan.Tools.AssetsBuilder.Logging;

namespace Titan.Tools.AssetsBuilder.Converters.Textures
{
    internal class TextureExporter : ITextureExporter
    {
        private readonly IByteWriter _byteWriter;
        private readonly ILogger _logger;

        public TextureExporter(IByteWriter byteWriter, ILogger logger)
        {
            _byteWriter = byteWriter;
            _logger = logger;
        }

        public void Export(in Pixel[] pixels, in string filename, bool overwrite)
        {
            using var file = File.OpenWrite(filename);
            file.SetLength(0);
            // TOOD: add header if we want to remove some channels from the image (like just export a single channel image)

            _logger.WriteLine($"Writing pixels {pixels.Length}");
            _byteWriter.Write(file, pixels);
        }
    }
}
