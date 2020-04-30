using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;

namespace Titan.Windows.Input
{
    public class InputManager : IInputManager
    {
        public IKeyboard Keyboard => _keyboard;
        public IMouse Mouse => _mouse;

        private readonly Keyboard _keyboard;
        private readonly Mouse _mouse;
        public InputManager(IEventManager eventManager)
        {
            eventManager.Subscribe<FixedUpdateEvent>(OnUpdate);

            _keyboard = new Keyboard(eventManager);
            _mouse = new Mouse(eventManager);
        }

        private void OnUpdate(in FixedUpdateEvent @event)
        {
            _keyboard.Update();
            _mouse.Update();
        }
    }
}
