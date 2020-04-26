using System;

namespace Titan.Windows.Window
{
    public interface IWindow
    {
        IntPtr Handle { get; }
        int Width { get; }
        int Height { get; }

        bool Windowed { get; }
        void SetTitle(string title);
        void HideWindow();
        void ShowWindow();
        bool GetMessage(ref Message message);
    }
}
