using System;
using Titan.Windows.Window;

namespace Titan.Graphics.Stuff
{
    public interface IDisplay : IDisposable
    {
        IDevice Device { get; }
        IWindow Window { get; }

        bool Update();
    }
}
