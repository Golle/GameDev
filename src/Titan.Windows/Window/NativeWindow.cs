using System;
using Titan.Windows.Win32;

namespace Titan.Windows.Window
{
    internal class NativeWindow : IWindow
    {
        public IntPtr Handle { get; }
        public int Width { get; }
        public int Height { get; }
        public bool Windowed => true; // This will change later

        public NativeWindow(IntPtr handle, int width, int height)
        {
            Handle = handle;
            Width = width;
            Height = height;
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

        public bool GetMessage(ref Message message)
        {
            MSG msg = default;
            if (!User32.PeekMessageA(ref msg, Handle, 0, 0, 1))
            {
                return false;
            }
            message.Value = msg.Message;
            
            User32.TranslateMessage(ref msg);
            User32.DispatchMessage(ref msg);
            return true;
        }
    }
}
