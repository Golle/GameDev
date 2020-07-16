using System;

namespace Titan.Windows.Window
{
    public interface IWindow : IDisposable
    {
        IntPtr Handle { get; }
        int Width { get; }
        int Height { get; }

        bool Windowed { get; }
        void SetTitle(string title);
        void ShowCursor(bool show);
        void HideWindow();
        void ShowWindow();
        bool Update();
    }
}
