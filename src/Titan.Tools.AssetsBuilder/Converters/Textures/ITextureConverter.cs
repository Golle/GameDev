using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder.Converters.Textures
{
    internal interface ITextureConverter
    {
        Pixel[] Convert(string filename);
    }
}
