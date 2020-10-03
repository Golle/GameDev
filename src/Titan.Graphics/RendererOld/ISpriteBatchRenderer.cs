using System;
using System.Numerics;
using Titan.D3D11;
using Titan.Graphics.Textures;

namespace Titan.Graphics.RendererOld
{
    public interface ISpriteBatchRenderer : IDisposable
    {
        void Push(ITexture2D texture2D, in TextureCoordinates textureCoordinates, in Vector2 position, in Vector2 size, in Color color);
        void Flush();

        void Render();
    }
}
