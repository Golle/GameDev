using Titan.Core.Assets.Images;

namespace Titan.Core.Assets.Fonts
{
    public class FontAsset
    {
        public int Width { get; }
        public int Height {  get; }
        public char[] Characters { get; }
        public ImageAsset Sheet { get; }
        public FontAsset(int width, int height, char[] characters, ImageAsset sheet)
        {
            Width = width;
            Height = height;
            Characters = characters;
            Sheet = sheet;
        }
    }
}
