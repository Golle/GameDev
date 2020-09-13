using System;
using Titan.Graphics.Renderer.Passes;

namespace Titan.Graphics.Textures
{
    public interface ITexture2D : IShaderResourceView, IDisposable
    {
        public uint Width { get; }
        public uint Height { get; }
        public IntPtr TextureHandle { get; }
    }
}
