using System;

namespace Titan.Windows.Window
{
    public interface IWindow
    {
        IntPtr Handle { get; }
        void SetTitle(string title);
        void HideWindow();
        void ShowWindow();
        bool GetMessage(ref Message message);
    }
}
