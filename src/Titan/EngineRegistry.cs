using Titan.Configuration;
using Titan.Core.Ioc;
using Titan.Resources;

namespace Titan
{
    internal class EngineRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IEngineConfigurationHandler, EngineConfigurationHandler>()

                .Register<ITextureManager, TextureManager>()
                ;
        }
    }
}
