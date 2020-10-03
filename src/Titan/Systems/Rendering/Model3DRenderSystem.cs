using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Renderer;
using Titan.Graphics.RendererOld;

namespace Titan.Systems.Rendering
{
    internal class Model3DRenderSystem : EntitySystem
    {
        private readonly IRenderQueue _renderQueue;
        private readonly Renderer3Dv2 _renderer;
        private readonly IComponentMap<Model3D> _model;
        private readonly IComponentMap<Transform3D> _transform;
        private readonly IComponentMap<Texture2D> _texture;

        public Model3DRenderSystem(IWorld world, IRenderQueue renderQueue, Renderer3Dv2 renderer) 
            : base(world, world.EntityFilter().With<Transform3D>().With<Model3D>().With<Texture2D>()) // TODO: add support for materials
        {
            _renderQueue = renderQueue;
            _renderer = renderer;
            _model = Map<Model3D>();
            _transform = Map<Transform3D>();
            _texture = Map<Texture2D>();
        }

        protected override void OnPreUpdate()
        {
            _renderer.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            _renderer.Begin();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var model = ref _model[entityId];
            ref var transform = ref _transform[entityId];
            ref var texture = ref _texture[entityId];
            //_renderQueue.Submit(model.Mesh, texture.Texture, transform.WorldTransform);
            _renderer.Render(model.Mesh, transform.WorldTransform, texture.Texture);
        }

        protected override void OnPostUpdate()
        {
            _renderer.End();
            //_renderQueue.Execute();
        }
    }
}
