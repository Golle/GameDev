using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Blobs;

namespace Titan.Systems.Rendering
{
    internal class D3D11ShaderLoaderSystem : BaseSystem
    {
        private readonly IDevice _device;
        private readonly ILogger _logger;
        private readonly IComponentMapper<Shader> _shader;
        private readonly IComponentMapper<D3D11Shader> _d3dShader;
        private readonly IBlobReader _blobReader;

        public D3D11ShaderLoaderSystem(IComponentManager componentManager, IBlobReader blobReader, IDevice device, ILogger logger)
            : base(typeof(Shader))
        {
            _blobReader = blobReader;
            _device = device;
            _logger = logger;

            _shader = componentManager.GetComponentMapper<Shader>();
            _d3dShader = componentManager.GetComponentMapper<D3D11Shader>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var shader = ref _shader[entity];
                _logger.Debug("Loading pixelshader from {0}", shader.PixelShader);
                _logger.Debug("Loading vertexshader from {0}", shader.VertexShader);

                // add the d3d shaders and create component 
                // TODO: figure out how to manage the lifetime of these
                ref var d3dShader = ref _d3dShader.CreateComponent(entity);
                using var vertexShaderBlob = _blobReader.ReadFromFile(shader.VertexShader); //TODO: extract this to a resource manager
                using var pixelShaderBlob = _blobReader.ReadFromFile(shader.PixelShader); //TODO: extract this to a resource manager
                
                d3dShader.VertexShader= _device.CreateVertexShader(vertexShaderBlob);
                d3dShader.PixelShader = _device.CreatePixelShader(pixelShaderBlob);
                d3dShader.InputLayout = _device.CreateInputLayout(ColoredVertex.VertexLayout, vertexShaderBlob);
                
                // remove the mesh component from the entity
                _shader.DestroyComponent(entity);
            }
        }
    }
}
