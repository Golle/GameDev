using Titan.Core.EventSystem;

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
            _keyboard = new Keyboard(eventManager);
            _mouse = new Mouse(eventManager);
        }

        public void Update()
        {
            _keyboard.Update();
            _mouse.Update();
        }
    }
}
