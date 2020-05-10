namespace Titan.Windows.Window
{
    public class CreateWindowArguments
    {
        public string Title { get; }
        public int Width { get; }
        public int Height { get; }
        public int X { get; } = -1; // Let windows decide position
        public int Y { get; } = -1; // Let windows decide position
        public CreateWindowArguments(string title, int width, int height)
        {
            Title = title;
            Width = width;
            Height = height;
        }
    }
}
