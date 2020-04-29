using System;
using Titan.Windows.Input;
using Titan.Windows.Win32;

namespace Titan.Windows.Window
{
    internal class NativeWindow : IWindow
    {
        public IKeyboard Keyboard => _keyboard;
        public IMouse Mouse => _mouse;
        public IntPtr Handle { get; internal set; }
        public int Width { get; }
        public int Height { get; }
        public bool Windowed => true; // This will change later
        
        // Create a delegate that have the same lifetime as the native window to prevent the GC to move it
        public User32.WndProcDelegate WindowProcedureDelegate { get; }
        private readonly NativeKeyboard _keyboard;
        private readonly NativeMouse _mouse;

        public NativeWindow(int width, int height)
        {
            Width = width;
            Height = height;
            _keyboard = new NativeKeyboard();
            _mouse = new NativeMouse();
            WindowProcedureDelegate = WindowProcedure;
        }

        public void SetTitle(string title)
        {
            User32.SetWindowTextA(Handle, title);
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


                // Mouse events, we can use wParam to read the state for mouse buttons. Might be better?

                case WindowsMessage.WM_MOUSEMOVE:
                    _mouse.OnMouseMove(GetMouseCoordinates((int)lParam));
                    break;
                case WindowsMessage.WM_LBUTTONDOWN:
                    _mouse.OnLeftMouseButtonDown(GetMouseCoordinates((int)lParam));
                    break;
                case WindowsMessage.WM_LBUTTONUP:
                    _mouse.OnLeftMouseButtonUp(GetMouseCoordinates((int)lParam));
                    break;
                case WindowsMessage.WM_RBUTTONDOWN:
                    _mouse.OnRightMouseButtonDown(GetMouseCoordinates((int)lParam));
                    break;
                case WindowsMessage.WM_RBUTTONUP:
                    _mouse.OnRightMouseButtonUp(GetMouseCoordinates((int)lParam));
                    break;

                // Add support to track mouse outside of window
                case WindowsMessage.WM_MOUSELEAVE:
                    break;
                case WindowsMessage.WM_MOUSEWHEEL:

                    break;
                //case WindowsMessage.WM_LBUTTONDBLCLK:
                //case WindowsMessage.WM_RBUTTONDBLCLK:
            }

            return User32.DefWindowProcA(hWnd, message, wParam, lParam);
        }

        private static (int x, int y) GetMouseCoordinates(int lParam)
        {
            return (lParam & 0xffff, (lParam >> 16) & 0xffff);
        }

        public void Dispose()
        {
            User32.DestroyWindow(Handle);
        }
    }
}
