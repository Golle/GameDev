using System;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.Windows.Input;
using Titan.Windows.Win32;
using Titan.Windows.Window.Events;

namespace Titan.Windows.Window
{
    internal class NativeWindow : IWindow
    {
        public IntPtr Handle { get; internal set; }
        public int Width { get; }
        public int Height { get; }
        public bool Windowed => true; // This will change later
        
        public IntPtr WindowProcedureFunctionPointer { get; }

        private readonly IEventManager _eventManager;

        private POINT _mousePosition = default;

        // Create a delegate that have the same lifetime as the native window to prevent the GC to move it
        private User32.WndProcDelegate _windowProcedureDelegate;
        private GCHandle _windowsProcedureHandle;
        public NativeWindow(int width, int height, IEventManager eventManager)
        {
            _eventManager = eventManager;
            Width = width;
            Height = height;
            _windowProcedureDelegate = WindowProcedure;
            WindowProcedureFunctionPointer = Marshal.GetFunctionPointerForDelegate((User32.WndProcDelegate)WindowProcedure);
            _windowsProcedureHandle = GCHandle.Alloc(WindowProcedureFunctionPointer, GCHandleType.Pinned);
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
            while (User32.PeekMessageA(out var msg, IntPtr.Zero, 0, 0, 1))
            {
                if (msg.Message == WindowsMessage.WM_QUIT)
                {
                    return false;
                }
                User32.TranslateMessage(ref msg);
                User32.DispatchMessage(ref msg);
            }
            UpdateMousePosition();
            return true;
        }

        private void UpdateMousePosition()
        {
            if (!User32.GetCursorPos(out var point))
            {
                return;
            }

            if (!User32.ScreenToClient(Handle, ref point))
            {
                return;
            }

            if (_mousePosition.X != point.X || _mousePosition.Y != point.Y)
            {
                _mousePosition = point;
                _eventManager.PublishImmediate(new MouseMovedEvent(_mousePosition.X, _mousePosition.Y));
            }
        }

        internal IntPtr WindowProcedure(IntPtr hWnd, WindowsMessage message, UIntPtr wParam, UIntPtr lParam)
        {
            switch (message)
            {
                case WindowsMessage.WM_CLOSE:
                    User32.PostQuitMessage(0);
                    return IntPtr.Zero;
                case WindowsMessage.WM_KILLFOCUS:
                    _eventManager.Publish(new WindowLostFocusEvent());
                    break;
                case WindowsMessage.WM_KEYDOWN:
                case WindowsMessage.WM_SYSKEYDOWN:
                    KeyDown((KeyCode) wParam, (lParam.ToUInt32() & 0x40000000) > 0);
                    break;
                case WindowsMessage.WM_KEYUP:
                case WindowsMessage.WM_SYSKEYUP:
                    _eventManager.Publish(new KeyReleasedEvent((KeyCode)wParam));
                    break;
                case WindowsMessage.WM_CHAR:
                    _eventManager.Publish(new CharacterTypedEvent((char)wParam));
                    break;


                // Mouse events, we can use wParam to read the state for mouse buttons. Might be better?


                // VM_MOSEMOVE is slow, look at GetCursorPos and ScreenToClient
                //case WindowsMessage.WM_MOUSEMOVE:
                //    _eventManager.Publish(new MouseMovedEvent(GetMouseCoordinates((int)lParam)));
                //    break;
                case WindowsMessage.WM_LBUTTONDOWN:
                    _eventManager.Publish(new MouseLeftButtonPressedEvent(GetMouseCoordinates((int)lParam)));
                    break;
                case WindowsMessage.WM_LBUTTONUP:
                    _eventManager.Publish(new MouseLeftButtonReleasedEvent(GetMouseCoordinates((int)lParam)));
                    break;
                case WindowsMessage.WM_RBUTTONDOWN:
                    _eventManager.Publish(new MouseRightButtonPressedEvent(GetMouseCoordinates((int)lParam)));
                    break;
                case WindowsMessage.WM_RBUTTONUP:
                    _eventManager.Publish(new MouseRightButtonReleasedEvent(GetMouseCoordinates((int)lParam)));
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

        private void KeyDown(KeyCode keyCode, bool repeat)
        {
            if (repeat)
            {
                _eventManager.Publish(new KeyAutoRepeatEvent(keyCode));
            }
            else
            {
                _eventManager.Publish(new KeyPressedEvent(keyCode));
            }
        }

        private static (int x, int y) GetMouseCoordinates(int lParam)
        {
            return (lParam & 0xffff, (lParam >> 16) & 0xffff);
        }

        public void Dispose()
        {
            if (_windowsProcedureHandle.IsAllocated)
            {
                _windowsProcedureHandle.Free();
            }
            User32.DestroyWindow(Handle);
        }
    }
}
