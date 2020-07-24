using System;
using System.Collections.Generic;
using Titan.Core.Assets.Fonts;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
    public interface IFontLoader
    {
        IFont Load(string filename);
    }

    internal class FontLoader : IFontLoader
    {
        private readonly IFontAssetLoader _fontAssetLoader;
        private readonly IDevice _device;

        public FontLoader(IFontAssetLoader fontAssetLoader, IDevice device)
        {
            _fontAssetLoader = fontAssetLoader;
            _device = device;
        }

        public IFont Load(string filename)
        {
            var font = _fontAssetLoader.LoadFromFile(filename);
            var imageAsset = font.Sheet;
            var texture2D = _device.CreateTexture2D(imageAsset.Width, imageAsset.Height, imageAsset.Pixels);

            Console.WriteLine($"[{imageAsset.Width}, {imageAsset.Height}]");
            var characterIndex = 0;
            var textureCoordinates = new Dictionary<char, TextureCoordinates>();
            for (var y = 0; y <= imageAsset.Height - font.Height; y += font.Height)
            {
                for (var x = 0; x <= imageAsset.Width - font.Width; x += font.Width)
                {
                    if (characterIndex >= font.Characters.Length)
                    {
                        break;
                    }

                    
                    Console.WriteLine($" Char: {font.Characters[characterIndex]}  [{x}, {y}] - [{x+font.Width}, {y+font.Height}]");
                    characterIndex++;
                }
            }

            return null;
        }
        
    }
}
