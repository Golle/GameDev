namespace Titan.Core.EventSystem
{
    public enum EventType : int
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
        EntityFreed,
        EntityDisabled,
        EntityEnabled,
        EntityAttached, 
        EntityDetached,

        NumberOfEvents // must be the last one

        
    }
}
