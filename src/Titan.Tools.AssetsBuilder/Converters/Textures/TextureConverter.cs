using System.IO;
using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.Images;

namespace Titan.Tools.AssetsBuilder.Converters.Textures
{
    internal class TextureConverter : ITextureConverter
    {
        private readonly ImageLoader _imageLoader;

        public TextureConverter(ImageLoader imageLoader)
        {
            _imageLoader = imageLoader;
        }
        
        public Pixel[] Convert(string filename)
        {
            var pixels = _imageLoader.LoadImage(filename);

            // TODO: add logic to convert into some nice format (DDS?)
            // Right now we only use the pixels from the BMP

            return pixels;
        }
    }
}
