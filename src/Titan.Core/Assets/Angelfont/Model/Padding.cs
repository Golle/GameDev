namespace Titan.Core.Assets.Angelfont.Model
{
    public struct Padding
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public int Horizontal => Right + Left;
        public int Vertical => Top + Bottom;

        public Padding(int top, int right, int bottom, int left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
