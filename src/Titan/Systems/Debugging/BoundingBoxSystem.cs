using System.Numerics;
using Titan.Components;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;
using Titan.Windows.Input;

namespace Titan.Systems.Debugging
{
    internal class BoundingBoxSystem : EntitySystem
    {
        private readonly RendererDebug3Dv3 _renderer;
        private readonly IInputManager _inputManager;
        private readonly IComponentMap<Transform3D> _transform;
        private readonly IComponentMap<Model3D> _model;

        public BoundingBoxSystem(IWorld world, RendererDebug3Dv3 renderer, IInputManager inputManager) : base(world, world.EntityFilter().With<Transform3D>().With<Model3D>())
        {
            _renderer = renderer;
            _inputManager = inputManager;
            _transform = Map<Transform3D>();
            _model = Map<Model3D>();
        }

        private bool _enabled = true;
        protected override void OnPreUpdate()
        {
            if (_inputManager.Keyboard.IsKeyReleased(KeyCode.One))
            {
                _enabled = !_enabled;
            }
        }

        protected override void OnPostUpdate()
        {
            if (_enabled)
            {
                _renderer.Render();
            }
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            if (!_enabled)
            {
                return;
            }
            ref var transform = ref _transform[entityId];
            ref var model = ref _model[entityId];
            
            _renderer.DrawBox(Vector3.Transform(model.Mesh.Min, transform.WorldTransform), Vector3.Transform(model.Mesh.Max, transform.WorldTransform), new Color{A = 1f, B = 1f, G = 1f, R = 0});
        }
    }
}
