namespace Titan.Core.EventSystem
{
    public enum EventType
    {
        MouseDown,
        KeyDown,
        KeyReleased,
        KeyAutoRepeat,
        CharacterTyped,

        LostFocus,
        WindowResize,

        Update,
        FixedUpdate,

        MouseMoved,
        MouseLeftButtonPressed,
        MouseLeftButtonReleased,
        MouseRightButtonReleased,
        MouseRightButtonPressed,


        ComponentAdded,
        ComponentRemoved,
        ComponentEnabled,
        ComponentDisabled,

        NumberOfEvents // must be the last one
    }
}
