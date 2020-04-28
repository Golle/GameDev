using System;
using Titan.Windows.Input;
using Titan.Windows.Win32;

namespace Titan.Windows.Window
{
    internal class NativeWindow : IWindow
    {
        public IKeyboard Keyboard => _keyboard;
        public IntPtr Handle { get; internal set; }
        public int Width { get; }
        public int Height { get; }
        public bool Windowed => true; // This will change later
        
        // Create a delegate that have the same lifetime as the native window to prevent the GC to move it
        public User32.WndProcDelegate WindowProcedureDelegate { get; }
        private NativeKeyboard _keyboard;
        public NativeWindow(int width, int height)
        {
            Width = width;
            Height = height;
            _keyboard = new NativeKeyboard();
            WindowProcedureDelegate = WindowProcedure;
        }

        public void SetTitle(string title)
        {

        }

        public void HideWindow()
        {
            User32.ShowWindow(Handle, Win32.ShowWindow.SW_HIDE);
        }

        public void ShowWindow()
        {
            User32.ShowWindow(Handle, Win32.ShowWindow.SW_SHOW);
        }

        public bool Update()
        {
            _keyboard.Update();
            while (User32.PeekMessageA(out var msg, IntPtr.Zero, 0, 0, 1))
            {
                if (msg.Message == WindowsMessage.WM_QUIT)
                {
                    return false;
                }
                User32.TranslateMessage(ref msg);
                User32.DispatchMessage(ref msg);
            }
            return true;
        }

        internal IntPtr WindowProcedure(IntPtr hWnd, WindowsMessage message, UIntPtr wParam, UIntPtr lParam)
        {
            switch (message)
            {
                case WindowsMessage.WM_CLOSE:
                    User32.PostQuitMessage(0);
                    return IntPtr.Zero;
                case WindowsMessage.WM_KILLFOCUS:
                    _keyboard.ClearState();
                    break;
                case WindowsMessage.WM_KEYDOWN:
                case WindowsMessage.WM_SYSKEYDOWN:
                    _keyboard.OnKeyDown((KeyCode)wParam);
                    break;
                case WindowsMessage.WM_KEYUP:
                case WindowsMessage.WM_SYSKEYUP:
                    _keyboard.OnKeyUp((KeyCode)wParam);
                    break;
                case WindowsMessage.WM_CHAR:
                    _keyboard.OnChar((char)wParam);
                    break;
            }

            return User32.DefWindowProcA(hWnd, message, wParam, lParam);
        }

        public void Dispose()
        {
            User32.DestroyWindow(Handle);
        }
    }
}
