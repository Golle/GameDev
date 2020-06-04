using Titan.Core.Ioc;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Camera;
using Titan.Graphics.Renderer;
using Titan.Graphics.Stuff;
using Titan.Graphics.Textures;

namespace Titan.Graphics
{
    public class GraphicsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .AddRegistry<D3D11Registry>()

                .Register<IGraphicsHandler, GraphicsHandler>()
                .Register<IDisplayFactory, DisplayFactory>()
                .Register<IBlobReader, BlobReader>()
                .Register<ICameraFactory, CameraFactory>()
                .Register<IRenderer, Renderer3D>()
                .Register<ISpriteBatchRenderer, SpriteBatchRenderer>()


                .Register<ITextureLoader, TextureLoader>()

                ;
        }
    }
}
