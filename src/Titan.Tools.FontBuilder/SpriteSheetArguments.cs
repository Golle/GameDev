using System.Drawing;
using System.Drawing.Text;

namespace Titan.Tools.FontBuilder
{
    public struct SpriteSheetArguments
    {
        public int Width;
        public int Height;
        public int FontSize;
        public Padding Padding;
        public string FontName;
        public FontStyle FontStyle;
        public TextRenderingHint Rendering;
    }
}
