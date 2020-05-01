namespace Titan.Windows.Window
{
    public class CreateWindowArguments
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public int X { get; set; } = -1; // Let windows decide position
        public int Y { get; set; } = -1; // Let windows decide position
    }
}
