using Titan.Core.Assets.Angelfont;
using Titan.Core.Assets.Fonts;
using Titan.Core.Assets.Images;
using Titan.Core.Assets.Wave;
using Titan.Core.Assets.WavefrontObj;
using Titan.Core.Assets.WavefrontObj.Parsers;
using Titan.Core.Ioc;

namespace Titan.Core.Assets
{
    internal class AssetsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {

            container
                
                .Register<IImageLoader, ImageLoader>()

                .Register<IFontAssetLoader, FontAssetLoader>()

                .Register<IObjLoader, ObjLoader>()
                .Register<ITextureParser, TextureParser>()
                .Register<IVertexParser, VertexParser>()
                .Register<INormalParser, NormalParser>()
                .Register<IFaceParser, FaceParser>()


                .Register<IAngelfontLoader, AngelfontLoader>()
                .Register<IAngelfontParser, AngelfontParser>()

                .Register<IWaveReader, WaveReader>()
                ;
        }
    }
}
