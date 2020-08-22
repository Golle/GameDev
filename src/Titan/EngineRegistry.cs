using Titan.Configuration;
using Titan.Core.Ioc;
using Titan.Resources;
using Titan.Scenes;

namespace Titan
{
    internal class EngineRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IEngineConfigurationHandler, EngineConfigurationHandler>()

                .Register<TextureManager>()
                //.Register<PixelShaderManager>() // TODO: add support for this later
                //.Register<VertexShaderManager>()
                .Register<MeshManager>()
                .Register<FontManager>()
                .Register<SoundClipManager>()
                .Register<ISceneParser, SceneParser>()
                ;
        }
    }
}
