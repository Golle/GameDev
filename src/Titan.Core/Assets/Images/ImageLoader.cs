using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Titan.Core.Logging;

namespace Titan.Core.Assets.Images
{
    internal class ImageLoader : IImageLoader
    {
        private readonly ILogger _logger;
        public ImageLoader(ILogger logger)
        {
            _logger = logger;
        }
        public ImageAsset LoadFromFile(string filename)
        {
            try
            {
                using var image = Image.FromFile(filename);
                using var bitmap = new Bitmap(image);

                var bytes = new List<byte>(bitmap.Width * bitmap.Height * 4);
                for (var x = 0; x < bitmap.Width; ++x)
                {
                    for (var y = 0; y < bitmap.Height; ++y)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        bytes.Add(pixel.R);
                        bytes.Add(pixel.G);
                        bytes.Add(pixel.B);
                        bytes.Add(pixel.A);
                    }
                }
                return new ImageAsset((uint) image.Height, (uint) image.Width, bytes.ToArray());
            }
            catch (IOException)
            {
                _logger.Error($"Failed to load image from {filename}");
                throw;
            }
        }
    }
}
