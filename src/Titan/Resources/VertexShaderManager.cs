using System.Collections.Generic;
using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Blobs;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Resources
{

    public enum InputLayoutTypes
    {
        ColoredVertex
    }

    // TODO: this is a bad design, should create another layer of abstraction with its own handling of internal D3D11 stuff like InputLayout, Shaders 
    // Maybe something like Model3DManager : ResourceManager<string, IModel3D>
    // MaterialManager : ResourceManager<string, IMaterial>
    // Which uses underlying managers for loading and compiling shaders, input layouts, textures etc.
    internal class VertexShaderManager : ResourceManager<string, IVertexShader>
    {
        private readonly IBlobReader _blobReader;
        private readonly IDevice _device;
        private readonly ILogger _logger;

        public VertexShaderManager(IBlobReader blobReader, IDevice device, ILogger logger)
        {
            _blobReader = blobReader;
            _device = device;
            _logger = logger;
        }

        protected override IVertexShader Load(in string filename)
        {
            _logger.Debug("Loading VertexShader from {0}", filename);

            using var vertexShaderBlob = _blobReader.ReadFromFile(filename);
            return _device.CreateVertexShader(vertexShaderBlob);
        }

        protected override void Unload(in string identifier, IVertexShader vertexShader)
        {
            _logger.Debug("Unloading resource at {0}", identifier);
            vertexShader.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, IVertexShader vertexShader)
        {
            entity.AddComponent(new VertexShader { Shader = vertexShader });

        }
    }
}
