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
        private readonly IComponentMap<Texture2D> _texture;

        public SpriteRenderSystem(IWorld world, ISpriteBatchRenderer spriteBatchRenderer) :
            base(world, world.EntityFilter().With<Transform2D>().With<Sprite>().With<Texture2D>())
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = Map<Transform2D>();
            _sprite = Map<Sprite>();
            _texture = Map<Texture2D>();
        }

        protected override void OnPostUpdate()
        {
            _spriteBatchRenderer.Flush();
            _spriteBatchRenderer.Render();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var sprite = ref _sprite[entityId];
            ref var transform = ref _transform[entityId];
            ref var texture = ref _texture[entityId];

            _spriteBatchRenderer.Push(texture.Texture, transform.WorldPosition, new Vector2(50, 50), sprite.Color);
        }
    }
}
