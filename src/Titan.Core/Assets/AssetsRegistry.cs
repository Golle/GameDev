using Titan.Core.Assets.Images;
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
                
                .Register<IAssetLoader, AssetLoader>()
                .Register<IImageLoader, ImageLoader>()

                .Register<IObjLoader, ObjLoader>()
                .Register<ITextureParser, TextureParser>()
                .Register<IVertexParser, VertexParser>()
                .Register<INormalParser, NormalParser>()
                .Register<IFaceParser, FaceParser>()
                ;
        }
    }
}
