namespace Titan.Windows.Input
{
    public interface IKeyboard
    {
        bool IsKeyDown(KeyCode keyCode);
        bool IsKeyUp(KeyCode keyCode);
        bool IsKeyReleased(KeyCode keyCode);
        bool TryGetChar(out char character);
    }
}
