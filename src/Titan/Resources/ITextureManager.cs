using Titan.Graphics.Textures;

namespace Titan.Resources
{
    public interface ITextureManager
    {
        ITexture2D GetTexture(string filename);
        void ReleaseTexture(string filename);
        void ReleaseAll();
    }
}
