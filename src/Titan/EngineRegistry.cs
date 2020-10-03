using Titan.IOC;
using Titan.Resources;
using Titan.Scenes;

namespace Titan
{
    internal class EngineRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<TextureResourceManager>()
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
