using System;
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
    internal class VertexShaderManager : ResourceManager<(string filename, VertexLayout vertexLayout), (IVertexShader vertexShader, IInputLayout inputLayout)>
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

        protected override (IVertexShader vertexShader, IInputLayout inputLayout) Load(in (string filename, VertexLayout vertexLayout) resource)
        {
            _logger.Debug("Loading VertexShader from {0}", resource.filename);

            var vertexShaderBlob = _blobReader.ReadFromFile(resource.filename);
            var vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            var inputLayout = _device.CreateInputLayout(resource.vertexLayout, vertexShaderBlob);

            return (vertexShader, inputLayout);
        }

        protected override void OnLoaded(Entity entity, (IVertexShader vertexShader, IInputLayout inputLayout) resource)
        {
            entity.AddComponent(new VertexShader {InputLayout = resource.inputLayout, Shader = resource.vertexShader});
        }
        protected override void Unload((IVertexShader vertexShader, IInputLayout inputLayout) resource)
        {
            _logger.Debug("Unloading VertexShader");
            resource.inputLayout.Dispose();
            resource.vertexShader.Dispose();
        }
    }
}
