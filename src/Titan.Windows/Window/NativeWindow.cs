using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.Core.Math;
using Titan.Windows.Input;
using Titan.Windows.Win32;
using Titan.Windows.Window.Events;

namespace Titan.Windows.Window
{
    internal class NativeWindow : IWindow
    {
        public IntPtr Handle { get; internal set; }
        public int Width => _windowSize.Width;
        public int Height => _windowSize.Height;
        public bool Windowed => true; // This will change later
        public IntPtr WindowProcedureFunctionPointer { get; }

        private Size _windowSize;
        private Point _windowCenter;
        private readonly IEventManager _eventManager;
        private Point _mousePosition;

        // Create a delegate that have the same lifetime as the native window to prevent the GC to move it
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly User32.WndProcDelegate _windowProcedureDelegate;
        private GCHandle _windowsProcedureHandle;
        private bool _cursorVisible = true;
        private Point _hideCursorPosition;

        public NativeWindow(int width, int height, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _windowSize = new Size(width, height);
            _windowCenter = new Point(width/2, height/2);
            _windowProcedureDelegate = WindowProcedure;
            WindowProcedureFunctionPointer = Marshal.GetFunctionPointerForDelegate(_windowProcedureDelegate);
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
            
            var mouseMoved = false;
            if (_cursorVisible)
            {
                if (!User32.ScreenToClient(Handle, ref point))
                {
                    return;
                }
                if (_mousePosition.X != point.X || _mousePosition.Y != point.Y)
                {
                    _mousePosition = point;
                    mouseMoved = true;
                }
            }
            else
            {
                var delta = point - _windowCenter;
                if (delta.X != 0 || delta.Y != 0)
                {
                    _mousePosition += delta;
                    mouseMoved = true;
                }
                User32.SetCursorPos(_windowCenter.X, _windowCenter.Y);
            }

            if (mouseMoved)
            {
                _eventManager.PublishImmediate(new MouseMovedEvent(_mousePosition.X, _mousePosition.Y));
            }
        }


        public void ShowCursor(bool show)
        {
            _cursorVisible = show;
            User32.ShowCursor(show);
            if (show)
            {
                User32.SetCursorPos(_hideCursorPosition.X, _hideCursorPosition.Y);
            }
            else
            {
                if (User32.GetCursorPos(out var pos))
                {
                    _hideCursorPosition = pos;
                }
                User32.SetCursorPos(_windowCenter.X, _windowCenter.Y);
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

                    // TODO: Temporary quit button to support graceful shutdown
                    if ((KeyCode) wParam == KeyCode.Escape)
                    {
                        User32.PostQuitMessage(0);
                    }
                    break;
                case WindowsMessage.WM_KEYUP:
                case WindowsMessage.WM_SYSKEYUP:
                    _eventManager.Publish(new KeyReleasedEvent((KeyCode)wParam));
                    break;
                case WindowsMessage.WM_CHAR:
                    _eventManager.Publish(new CharacterTypedEvent((char)wParam));
                    break;
                case WindowsMessage.WM_SIZE:
                    _windowSize = Split((int)lParam);
                    _windowCenter = new Point(_windowSize.Width/2, _windowSize.Height/2);
                    break;
                case WindowsMessage.WM_EXITSIZEMOVE:
                    _eventManager.Publish(new WindowResizeEvent(Width, Height));
                    break;

                // Mouse events, we can use wParam to read the state for mouse buttons. Might be better?
                // VM_MOSEMOVE is slow, look at GetCursorPos and ScreenToClient
                //case WindowsMessage.WM_MOUSEMOVE:
                //    _eventManager.Publish(new MouseMovedEvent(GetMouseCoordinates((int)lParam)));
                //    break;
                case WindowsMessage.WM_LBUTTONDOWN:
                    _eventManager.Publish(new MouseLeftButtonPressedEvent());
                    break;
                case WindowsMessage.WM_LBUTTONUP:
                    _eventManager.Publish(new MouseLeftButtonReleasedEvent());
                    break;
                case WindowsMessage.WM_RBUTTONDOWN:
                    _eventManager.Publish(new MouseRightButtonPressedEvent());
                    break;
                case WindowsMessage.WM_RBUTTONUP:
                    _eventManager.Publish(new MouseRightButtonReleasedEvent());
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

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private (int x, int y) GetMouseCoordinates(int lParam)
        //{
        //    return (lParam & 0xffff, (lParam >> 16) & 0xffff);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Split(int lParam)
        {
            return new Size(lParam & 0xffff, (lParam >> 16) & 0xffff);
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
