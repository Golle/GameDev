using System.Runtime.InteropServices;

namespace Titan.Core.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public int Width;
        public int Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public Size(int size)
        {
            Height = Width = size;
        }

        public static Size operator +(in Size size1, in Size size2)
        {
            return new Size(size1.Width + size2.Width, size2.Height + size2.Height);
        }
    }
}
