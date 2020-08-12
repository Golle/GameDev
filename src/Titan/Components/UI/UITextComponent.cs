using Titan.D3D11;

namespace Titan.Components.UI
{
    public struct UITextComponent
    {
        public string Text;
        public Color Color;

        public UITextComponent(string text, in Color color = default)
        {
            Text = text;
            Color = color;
        }
    }
}
