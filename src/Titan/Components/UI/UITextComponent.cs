using System.Runtime.InteropServices;
using Titan.D3D11;

namespace Titan.Components.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UITextComponent
    {
        public Color Color;
        public int LineHeight;
        public int FontSize;
        public string Text;

        public UITextComponent(string text, int fontSize = 0, int lineHeight = 0, in Color color = default)
        {
            Color = color;
            LineHeight = lineHeight;
            FontSize = fontSize;
            Text = text;
        }
    }
}
