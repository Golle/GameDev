using System;

namespace Titan.Graphics
{
    public interface IGraphicsHandler : IDisposable
    {
        bool Initialize(string title, int width, int height);
        bool Update();
    }
}
