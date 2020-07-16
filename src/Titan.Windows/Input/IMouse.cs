using Titan.Windows.Win32;

namespace Titan.Windows.Input
{
    public interface IMouse
    {
        Point Position { get; }
        Point DeltaMovement { get; }
        bool LeftButtonDown { get; }
        bool RightButtonDown { get; }
    }
}
