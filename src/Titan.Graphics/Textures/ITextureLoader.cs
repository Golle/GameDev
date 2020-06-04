namespace Titan.Graphics.Textures
{
    public interface ITextureLoader
    {
        ITexture2D LoadTexture(string filename);
    }
}
