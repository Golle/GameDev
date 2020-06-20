using System;

namespace Titan.Graphics.Textures
{
    public interface ISampler : IDisposable
    {
        void Bind(uint startSlot = 0u);
    }
}