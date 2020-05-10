using System;
using Titan.Windows.Window;

namespace Titan.D3D11.Device
{
    public class CreateDeviceArguments
    {
        public IWindow Window { get; }
        public IntPtr Adapter { get; }
        public uint RefreshRate { get;  }
        public bool Debug { get; }
        public CreateDeviceArguments(IWindow window, uint refreshRate, IntPtr adapter, bool debug)
        {
            Window = window;
            Adapter = adapter;
            RefreshRate = refreshRate;
            Debug = debug;
        }
    }
}
