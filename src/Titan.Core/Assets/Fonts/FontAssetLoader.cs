using System;
using System.IO;
using Titan.Core.Assets.Images;
using Titan.Core.Logging;

namespace Titan.Core.Assets.Fonts
{
    internal class FontAssetLoader : IFontAssetLoader
    {
        private readonly ILogger _logger;
        private readonly IImageLoader _imageLoader;

        public FontAssetLoader(ILogger logger, IImageLoader imageLoader)
        {
            _logger = logger;
            _imageLoader = imageLoader;
        }

        public FontAsset LoadFromFile(string filename)
        {
            _logger.Debug("Loading font from {0}", filename);
            try
            {
                using var file = File.OpenText(filename);
                int width = 0, height = 0;
                string? sheet = null;
                char[] characters = Array.Empty<char>();


                string? line;
                while (!string.IsNullOrWhiteSpace(line = file.ReadLine()))
                {
                    switch (line)
                    {
                        case var s when s.StartsWith("width"): width = int.Parse(s.Split(" ")[1]); break;
                        case var s when s.StartsWith("height"): height = int.Parse(s.Split(" ")[1]); break;
                        case var s when s.StartsWith("sheet"): sheet = s.Split(" ")[1]; break;
                        default: characters = line.ToCharArray(); break;
                    }
                }

                if (height <= 0) throw new ArgumentException($"{height} is not valid value", nameof(height));
                if (width <= 0) throw new ArgumentException($"{width} is not valid value", nameof(width));
                if (sheet == null) throw new ArgumentNullException($"No sheet has been set", nameof(sheet));
                if (characters.Length == 0) throw new ArgumentNullException($"No characters has been specified", nameof(characters));

                var fontDirectory = Path.GetDirectoryName(filename);
                if (fontDirectory == null) throw new InvalidOperationException($"Failed to get font directory from path {filename}");

                var image = _imageLoader.LoadFromFile(Path.Combine(fontDirectory, sheet));

                _logger.Debug("Font loaded with {0} characters at width {1} and height {2}. Sprite sheet used {3}", characters.Length, width, height, sheet);

                return new FontAsset(width, height, characters, image);

            }
            catch (IOException)
            {
                _logger.Error("Failed to load font from {0}", filename);
                throw;
            }
        }
    }
}
