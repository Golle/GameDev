using Titan.Core.Assets.Images;
using Titan.Core.Math;

namespace Titan.Core.Assets.Fonts
{
    public class FontAsset
    {
        public int LineHeight { get; }
        public int Baseline { get; }
        public ImageAsset FontBitmap { get; }
        public CharacterAsset[] Characters { get; }
        //private readonly IDictionary<(char, char), int> _kernings; // TODO: implement this in some nice way
        public FontAsset(int baseline, int lineHeight, ImageAsset fontBitmap, in CharacterAsset[] characters)
        {
            Baseline = baseline;
            LineHeight = lineHeight;
            FontBitmap = fontBitmap;
            Characters = characters;
        }
    }
}
