namespace Titan.Windows
{
    public readonly struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator -(in Point lh, in Point rh) => new Point(lh.X - rh.X, lh.Y - rh.Y);
    }
}
