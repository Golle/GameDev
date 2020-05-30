using System.Numerics;
using Titan.Components;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics;

namespace Titan.Systems.Rendering
{
    internal class D3D11ModelLoaderSystem : BaseSystem
    {
        private readonly IDevice _device;
        private readonly ILogger _logger;
        private IComponentMapper<Mesh> _mesh;
        private IComponentMapper<D3D11Model> _model;


        public D3D11ModelLoaderSystem(IComponentManager componentManager, IDevice device, ILogger logger)
            : base(typeof(Mesh))
        {
            _mesh = componentManager.GetComponentMapper<Mesh>();
            _model = componentManager.GetComponentMapper<D3D11Model>();
            _device = device;
            _logger = logger;
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                var fileName = _mesh[entity].Filename;
                _logger.Debug("Loading mesh from {0}", fileName);

                // add the d3d model and create components 
                // TODO: figure out how to manage the lifetime of these
                ref var model = ref _model.CreateComponent(entity);
                model.IndexBuffer = _device.CreateIndexBuffer(Indices);
                model.VertexBuffer = _device.CreateVertexBuffer(Model);
             
                // remove the mesh component from the entity
                _mesh.DestroyComponent(entity);
            }
        }

        private static readonly short[] Indices = {
            0, 2, 1, 2, 3, 1,
            1, 3, 5, 3, 7, 5,
            2, 6, 3, 3, 6, 7,
            4, 5, 7, 4, 7, 6,
            0, 4, 2, 2, 4, 6,
            0, 1, 4, 1, 5, 4
        };

        private static readonly ColoredVertex[] Model =
        {
            new ColoredVertex {Color = new Color(1f, 0, 0), Position = new Vector3(-1f, -1f, -1f)},
            new ColoredVertex {Color = new Color(1f, 1f, 0), Position = new Vector3(1f, -1f, -1f)},
            new ColoredVertex {Color = new Color(1f, 0, 1f), Position = new Vector3(-1f, 1f, -1f)},
            new ColoredVertex {Color = new Color(0f, 1f, 0), Position = new Vector3(1f, 1f, -1f)},
            new ColoredVertex {Color = new Color(0f, 1f, 1f), Position = new Vector3(-1f, -1f, 1f)},
            new ColoredVertex {Color = new Color(1f, 1f, 0), Position = new Vector3(1f, -1f, 1f)},
            new ColoredVertex {Color = new Color(1f, 1f, 1f), Position = new Vector3(-1f, 1f, 1f)},
            new ColoredVertex {Color = new Color(1f, 0, 0), Position = new Vector3(1f, 1f, 1f)},
        };
    }
}