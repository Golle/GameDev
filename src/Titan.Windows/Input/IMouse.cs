namespace Titan.Windows.Input
{
    public interface IMouse
    {
        Point Position { get; }
        bool LeftButtonDown { get; }
        bool RightButtonDown { get; }
    }
}
