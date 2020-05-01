using Titan.Core.Ioc;
using Titan.D3D11;

namespace Titan.Graphics
{
    public class GraphicsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .AddRegistry<D3D11Registry>()

                .Register<IGraphicsHandler, GraphicsHandler>()

                ;
        }
    }
}
