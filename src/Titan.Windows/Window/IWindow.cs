using System;
using Titan.Windows.Input;

namespace Titan.Windows.Window
{
    public interface IWindow : IDisposable
    {
        IKeyboard Keyboard { get; }

        IntPtr Handle { get; }
        int Width { get; }
        int Height { get; }

        bool Windowed { get; }
        void SetTitle(string title);
        void HideWindow();
        void ShowWindow();
        bool Update();
    }
}
