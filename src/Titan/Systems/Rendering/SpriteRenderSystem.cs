using System.Numerics;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class SpriteRenderSystem : EntitySystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMap<Transform2D> _transform;
        private readonly IComponentMap<Sprite> _sprite;

        public SpriteRenderSystem(IWorld world, ISpriteBatchRenderer spriteBatchRenderer) :
            base(world, world.EntityFilter().With<Transform2D>().With<Sprite>())
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = Map<Transform2D>();
            _sprite = Map<Sprite>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var sprite = ref _sprite[entityId];
            ref var transform = ref _transform[entityId];

            _spriteBatchRenderer.Push(sprite.Texture, transform.WorldPosition, new Vector2(50, 50), sprite.Color);
        }
    }
}
