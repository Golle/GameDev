using System.Linq;
using System.Numerics;
using Titan.Core.Assets.Fonts;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
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
            var texture2D = _device.CreateTexture2D(font.FontBitmap.Width, font.FontBitmap.Height, font.FontBitmap.Pixels);

            TextureCoordinates GetCoords(CharacterAsset asset)
            {
                var width = font.FontBitmap.Width;
                var height = font.FontBitmap.Height;
                var position = asset.Position;
                var size = asset.Size;
                return new TextureCoordinates
                {
                    TopLeft = new Vector2(position.X / width, 1f - position.Y / height),
                    BottomRight = new Vector2((position.X + size.Width) / width, 1f - (position.Y + size.Height) / height)
                };
            }

            // Translate offset to pixels on screen
            Vector2 GetOffset(CharacterAsset asset)
            {
                return new Vector2(asset.Offset.X, font.LineHeight - asset.Offset.Y - asset.Size.Height);
            }

            var characters = font
                .Characters
                .ToDictionary(c => c.Id, character => new Character(character.Size, GetOffset(character), character.AdvanceX, GetCoords(character)));

            return new Font(font.Baseline, font.LineHeight, font.FontSize, texture2D, characters, characters['?']); // TODO: use ? for FallbackCharacter, should specify it in some other way maybe? in case it doesn't exist in the sheet.
        }
    }
}
