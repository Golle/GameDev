using System;

namespace Titan.Graphics
{
    public interface IBlendState : IDisposable
    {
        void Bind();
    }
}
