using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.Windows.Win32;

namespace Titan.Windows.Window
{
    internal class WindowCreator : IWindowCreator
    {
        private readonly IEventManager _eventManager;

        public WindowCreator(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public IWindow CreateWindow(CreateWindowArguments arguments)
        {
            var nativeWindow = new NativeWindow(arguments.Width, arguments.Height, _eventManager);
            var wndClassExA = new WNDCLASSEXA
            {
                CbClsExtra = 0,
                CbSize = Marshal.SizeOf<WNDCLASSEXA>(),
                HCursor = IntPtr.Zero,
                HIcon = IntPtr.Zero,
                LpFnWndProc = nativeWindow.WindowProcedureFunctionPointer,
                CbWndExtra = 0,
                HIconSm = IntPtr.Zero,
                HInstance = Marshal.GetHINSTANCE(GetType().Module),
                HbrBackground = IntPtr.Zero,
                LpszClassName = arguments.Title + "class",
                Style = 0
            };
            if (User32.RegisterClassExA(ref wndClassExA) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "RegisterClassExA failed");
            }

            const WindowStyles wsStyle = WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_VISIBLE;

            RECT windowRect = default;
            windowRect.Left = 100;
            windowRect.Right = arguments.Width + windowRect.Left;
            windowRect.Top = 100;
            windowRect.Bottom = arguments.Height + windowRect.Top;
            User32.AdjustWindowRect(ref windowRect, wsStyle, false);
            
            nativeWindow.Handle = User32.CreateWindowExA(
                0, 
                wndClassExA.LpszClassName, 
                arguments.Title, 
                wsStyle, 
                arguments.X, 
                arguments.Y, 
                windowRect.Right - windowRect.Left, 
                windowRect.Bottom - windowRect.Top, 
                IntPtr.Zero, 
                IntPtr.Zero, 
                wndClassExA.HInstance, 
                IntPtr.Zero
                );

            if (nativeWindow.Handle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateWindowExA failed");
            }
            
            nativeWindow.ShowWindow();
            return nativeWindow;
        }
    }
}
