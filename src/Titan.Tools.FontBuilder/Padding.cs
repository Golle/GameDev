namespace Titan.Tools.FontBuilder
{
    public struct Padding
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public int Horizontal => Right + Left;
        public int Vertical => Top + Bottom;
    }
}
