using System;
using Titan.Windows.Window;

namespace Titan.D3D11.Device
{
    public class CreateDeviceArguments
    {
        public IWindow Window { get; set; }
        public IntPtr Adapter { get; set; }

        public uint RefreshRate { get; set; }
    }
}
