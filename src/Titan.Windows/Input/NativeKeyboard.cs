using System.Collections.Generic;

namespace Titan.Windows.Input
{
    internal class NativeKeyboard : IKeyboard
    {
        private readonly bool[] _previousState = new bool[(int)KeyCode.NumberOfKeys];
        private readonly bool[] _keyState = new bool[(int)KeyCode.NumberOfKeys];
        
        private readonly Queue<char> _charQueue = new Queue<char>(50);
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

        internal void OnKeyDown(KeyCode keyCode)
        {
            _previousState[(int) keyCode] = _keyState[(int) keyCode];
            _keyState[(int)keyCode] = true;
        }

        internal void OnKeyUp(KeyCode keyCode)
        {
            _previousState[(int)keyCode] = _keyState[(int)keyCode];
            _keyState[(int)keyCode] = false;
        }

        internal void OnChar(char character)
        {
            _charQueue.Enqueue(character);
        }

        internal void Update()
        {
            _charQueue.Clear();
            for (var i = 0; i < _previousState.Length; ++i)
            {
                _previousState[i] = false;
            }
        }

        internal void ClearState()
        {
            _charQueue.Clear();
            for (var i = 0; i < _keyState.Length; ++i)
            {
                _keyState[i] = false;
                _previousState[i] = false;
            }
        }
    }
}
