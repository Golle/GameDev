using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder.Converters.Textures
{
    internal interface ITextureExporter
    {
        void Export(in Pixel[] pixels, in string filename, bool overwrite);
    }
}
