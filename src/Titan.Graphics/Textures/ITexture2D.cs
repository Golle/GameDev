using System;
using System.Numerics;

namespace Titan.Graphics.Textures
{
    public interface ITexture2D : IDisposable
    {
        ref readonly Vector2 TopLeft { get; }
        ref readonly Vector2 BottomRight { get; }

        void Bind();
    }
}
