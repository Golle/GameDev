using System;

namespace Titan.Graphics
{
    public interface IDisplayFactory
    {
        IDisplay Create(string title, int width, int height);
        IDisplay Create(string title, int width, int height, IAdapter adapter);
    }
}
