using System;

namespace Titan.Graphics.Stuff
{
    public interface IGraphicsHandler : IDisposable
    {
        bool Initialize(string title, int width, int height);
        bool Update();
    }
}
