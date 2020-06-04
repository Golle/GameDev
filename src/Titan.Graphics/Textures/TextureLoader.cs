using Titan.Core.Assets.Images;
using Titan.Core.Logging;

namespace Titan.Graphics.Textures
{
    internal class TextureLoader : ITextureLoader
    {
        private readonly IImageLoader _imageLoader;
        private readonly IDevice _device;
        private readonly ILogger _logger;

        public TextureLoader(IImageLoader imageLoader, IDevice device, ILogger logger)
        {
            _imageLoader = imageLoader;
            _device = device;
            _logger = logger;
        }

        public ITexture2D LoadTexture(string filename)
        {
            _logger.Debug("Loading Texture2D from {0}", filename);
            var image = _imageLoader.LoadFromFile(filename);
            _logger.Debug("Texture2D loaded. Width: {0} Height: {1}", image.Width, image.Height);

            return _device.CreateTexture2D(image.Width, image.Height, image.Pixels);
        }
    }
}
