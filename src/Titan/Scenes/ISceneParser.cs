using System.IO;
using Titan.Core.Json;

namespace Titan.Scenes
{

    internal class SceneDescriptor
    {
        public SceneConfiguration Configuration { get; set; }
    }


    internal interface ISceneParser
    {
        SceneDescriptor Parse(string filename);
    }

    internal class SceneParser : ISceneParser
    {
        private readonly IJsonSerializer _jsonSerializer;
        
        public SceneParser(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        public SceneDescriptor Parse(string filename)
        {
            using var file = File.OpenText(filename);

            return _jsonSerializer.Deserialize<SceneDescriptor>(file.ReadToEnd());
        }
    }

    internal class SceneConfiguration
    {
        public uint ComponentPoolDefaultSize { get; set; }
        public uint MaxEntities { get; set; }
        public ComponentConfiguration[] Components { get; set; }
        public ResourceConfiguration[] Resources { get; set; }
        public string[] Systems { get; set; }

    }

    internal struct ResourceConfiguration
    {
        public string Identifier { get; set; }
        public string Type { get; set; }
    }

    internal struct ComponentConfiguration
    {
        public string Name { get; set; }
        public uint Size { get; set; }
    }
}
