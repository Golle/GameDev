using System;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    public interface ITextureManager : IDisposable
    {
        ITexture2D GetTexture(string filename);
        void ReleaseTexture(string filename);
        void ReleaseAll();
    }
}
