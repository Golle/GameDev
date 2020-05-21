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


        ComponentCreated,
        ComponentDestroyed,
        ComponentAdded,
        ComponentRemoved,
        ComponentEnabled,
        ComponentDisabled,

        EntityCreated,
        EntityDestroyed,
        EntityDisabled,
        EntityEnabled,

        NumberOfEvents // must be the last one


        ,
    }
}
