using Titan.Core.Ioc;

namespace Titan.Graphics.Renderer
{
    internal class RendererRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IRenderQueue, RenderQueue>();
        }
    }
}
