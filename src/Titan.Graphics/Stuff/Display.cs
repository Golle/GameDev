using System;
using Titan.Windows.Window;

namespace Titan.Graphics.Stuff
{
    internal class Display : IDisplay
    {
        public IDevice Device { get; }
        public IWindow Window { get; }

        public Display(IDevice device, IWindow window)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            Window = window ?? throw new ArgumentNullException(nameof(window));
        }

        public bool Update()
        {
            if (!Window.Update())
            {
                return false;
            }
            Device.BeginRender();

            Device.EndRender();
            return true;
        }

        public void Dispose()
        {
            Device.Dispose();
            Window.Dispose();
        }
    }
}
