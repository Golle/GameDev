using System;
using Titan.Windows.Window;

namespace Titan.Graphics
{
    public interface IDisplay : IDisposable
    {
        IDevice Device { get; }
        IWindow Window { get; }
    }
}
