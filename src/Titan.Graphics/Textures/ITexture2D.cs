using System;

namespace Titan.Graphics.Textures
{
    public interface ITexture2D : IDisposable
    {
        public uint Width { get; }
        public uint Height { get; }
        void Bind(uint startSlot = 0u);
    }
}
