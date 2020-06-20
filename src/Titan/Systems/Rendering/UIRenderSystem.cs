using System.Numerics;
using Titan.Components;
using Titan.D3D11;
using Titan.ECS3;
using Titan.ECS3.Components;
using Titan.ECS3.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class UIRenderSystem : EntitySystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMap<TransformRect> _transform;
        private readonly IComponentMap<Sprite> _sprite;

        public UIRenderSystem(IWorld world, ISpriteBatchRenderer spriteBatchRenderer) 
            : base( world, world.EntityFilter().With<TransformRect>().With<Sprite>())
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = Map<TransformRect>();
            _sprite = Map<Sprite>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var rect = ref _transform[entityId];
            ref var sprite = ref _sprite[entityId];

            _spriteBatchRenderer.Push(sprite.Texture, new Vector2(rect.Position.X, rect.Position.Y), new Vector2(rect.Size.Width, rect.Size.Height), new Color(1, 0, 0));
        }
    }
}
