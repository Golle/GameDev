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

        // Prevent the delegate from being garbage collected
        private static readonly User32.WndProcDelegate DefaultWindowProcedure = User32.DefWindowProcA;

        public WindowCreator(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public IWindow CreateWindow(CreateWindowArguments arguments)
        {
            var nativeWindow = new NativeWindow(arguments.Width, arguments.Height, _eventManager);
            var windowProcedure = Marshal.GetFunctionPointerForDelegate(nativeWindow.WindowProcedureDelegate);
            GCHandle.Alloc(windowProcedure, GCHandleType.Pinned);
            var wndClassExA = new WNDCLASSEXA
            {
                CbClsExtra = 0,
                CbSize = Marshal.SizeOf<WNDCLASSEXA>(),
                HCursor = IntPtr.Zero,
                HIcon = IntPtr.Zero,
                LpFnWndProc = windowProcedure,
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
            
            nativeWindow.Handle = User32.CreateWindowExA(
                0, 
                wndClassExA.LpszClassName, 
                arguments.Title, 
                WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_VISIBLE, 
                arguments.X, 
                arguments.Y, 
                arguments.Width, 
                arguments.Height, 
                IntPtr.Zero, 
                IntPtr.Zero, 
                wndClassExA.HInstance, 
                IntPtr.Zero
                );

            if (nativeWindow.Handle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateWindowExA failed");
            }
            return nativeWindow;
        }
    }
}
