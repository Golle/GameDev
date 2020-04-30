using System.Collections.Generic;
using Titan.Core.EventSystem;
using Titan.Windows.Window.Events;

namespace Titan.Windows.Input
{
    internal class Keyboard : IKeyboard
    {
        private readonly bool[] _previousState = new bool[(int)KeyCode.NumberOfKeys];
        private readonly bool[] _keyState = new bool[(int)KeyCode.NumberOfKeys];
        
        private readonly Queue<char> _charQueue = new Queue<char>(50);

        public Keyboard(IEventManager eventManager)
        {
            eventManager.Subscribe<KeyPressedEvent>(OnKeyPressed);
            eventManager.Subscribe<KeyReleasedEvent>(OnKeyReleased);
            eventManager.Subscribe<CharacterTypedEvent>(OnCharacter);
            eventManager.Subscribe<WindowLostFocusEvent>(ClearState);
        }

        private void ClearState(in WindowLostFocusEvent @event)
        {
            _charQueue.Clear();
            for (var i = 0; i < _keyState.Length; ++i)
            {
                _keyState[i] = false;
                _previousState[i] = false;
            }
        }

        private void OnCharacter(in CharacterTypedEvent @event)
        {
            _charQueue.Enqueue(@event.Character);
        }

        private void OnKeyReleased(in KeyReleasedEvent @event)
        {
            var key = (int)@event.Key;
            _previousState[key] = _keyState[key];
            _keyState[key] = false;
        }

        private void OnKeyPressed(in KeyPressedEvent @event)
        {
            var key = (int)@event.Key;
            _previousState[key] = _keyState[key];
            _keyState[key] = true;
        }

        public bool IsKeyDown(KeyCode keyCode)
        {
            return _keyState[(int)keyCode];
        }

        public bool IsKeyUp(KeyCode keyCode)
        {
            return !_keyState[(int)keyCode];
        }

        public bool IsKeyReleased(KeyCode keyCode)
        {
            return IsKeyUp(keyCode) && _previousState[(int) keyCode];
        }

        public bool TryGetChar(out char character)
        {
            return _charQueue.TryDequeue(out character);
        }

        internal void Update()
        {
            _charQueue.Clear();
            for (var i = 0; i < _previousState.Length; ++i)
            {
                _previousState[i] = false;
            }
        }
    }
}
