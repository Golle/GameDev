using Titan.Core.Ioc;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Camera;
using Titan.Graphics.Fonts;
using Titan.Graphics.Models;
using Titan.Graphics.RendererOld;
using Titan.Graphics.Textures;

namespace Titan.Graphics
{
    public class GraphicsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .AddRegistry<D3D11Registry>()

                .Register<IDisplayFactory, DisplayFactory>()
                .Register<IBlobReader, BlobReader>()
                .Register<ICameraFactory, CameraFactory>()
                .Register<ISpriteBatchRenderer, SpriteBatchRenderer>()
                .Register<Renderer3Dv2>()
                .Register<RendererDebug3Dv3>()
                
                .Register<IMeshLoader, MeshLoader>()
                .Register<ITextureLoader, TextureLoader>()
                .Register<IFontLoader, FontLoader>()

                ;
        }
    }
}
