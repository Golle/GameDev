namespace Titan.Windows.Input
{
    public interface IInputManager
    {
        IKeyboard Keyboard { get; }
        IMouse Mouse { get; }
        void Update();
    }
}
