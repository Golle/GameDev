using System;
using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Blobs;
using Titan.Graphics.Shaders;

namespace Titan.Resources
{
    internal class PixelShaderManager : ResourceManager<string, IPixelShader>
    {
        private readonly IBlobReader _blobReader;
        private readonly IDevice _device;
        private readonly ILogger _logger;

        public PixelShaderManager(IBlobReader blobReader, IDevice device, ILogger logger)
        {
            _blobReader = blobReader;
            _device = device;
            _logger = logger;
        }

        protected override IPixelShader Load(in string filename)
        {
            _logger.Debug("Loading PixelShader from {0}", filename);
            var blob = _blobReader.ReadFromFile(filename);
            return _device.CreatePixelShader(blob);
        }

        protected override void Unload(IPixelShader resource)
        {
            _logger.Debug("Unloading PixelShader");
            resource.Dispose();
        }

        protected override void OnLoaded(Entity entity, IPixelShader pixelShader)
        {
            entity.AddComponent(new PixelShader { Shader = pixelShader});
        }
    }
}
