using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Titan.Core.Assets.Angelfont;
using Titan.Core.Assets.Images;
using Titan.Core.Logging;
using Titan.Core.Math;

namespace Titan.Core.Assets.Fonts
{
    internal class FontAssetLoader : IFontAssetLoader
    {
        private readonly ILogger _logger;
        private readonly IImageLoader _imageLoader;
        private readonly IAngelfontLoader _angelfontLoader;

        public FontAssetLoader(ILogger logger, IImageLoader imageLoader, IAngelfontLoader angelfontLoader)
        {
            _logger = logger;
            _imageLoader = imageLoader;
            _angelfontLoader = angelfontLoader;
        }

        public FontAsset LoadFromFile(string filename)
        {
            _logger.Debug("Loading font from {0}", filename);
     
            var font = _angelfontLoader.LoadFromPath(filename);
            if(font.Pages.Length > 1) throw new NotSupportedException("More than a single page is currently not supported.");
            
            var directory = Path.GetDirectoryName(filename) ?? throw new InvalidOperationException($"Can't find directory for path {filename}");
            var bitmapPath = Path.Combine(directory, font.Pages[0].File);
            _logger.Debug("Loading font bitmap from {0}", bitmapPath);
            var image = _imageLoader.LoadFromFile(bitmapPath);
            if (image.Width != font.Common.ScaleWidth || image.Height != font.Common.ScaleHeight)
            {
                throw new InvalidOperationException($"Height or Width mismatch between .fnt and image asset. Width: {font.Common.ScaleWidth} - {image.Width}, Height: {font.Common.ScaleHeight} - {image.Height}");
            }

            var characters = font.Characters
                .Select(c => new CharacterAsset
                {
                    Id = (char)c.Id,
                    Position = new Vector2(c.X, c.Y),
                    AdvanceX = c.XAdvance,
                    Offset = new Vector2(c.XOffset, c.YOffset),
                    Size = new Size(c.Width, c.Height)
                })
                .ToArray();

            return new FontAsset(font.Common.Base, font.Common.LineHeight, font.Info.Size, image, characters);
        }
    }
}
