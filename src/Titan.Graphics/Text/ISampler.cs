using System;

namespace Titan.Graphics.Text
{
    public interface ISampler : IDisposable
    {
        void Bind(uint startSlot = 0u);
    }
}