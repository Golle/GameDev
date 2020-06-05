using System.Numerics;
using Titan.Components;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class SpriteRenderSystem : BaseSystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMapper<Transform2D> _transform;
        private readonly IComponentMapper<Sprite> _sprite;

        public SpriteRenderSystem(IComponentManager componentManager, ISpriteBatchRenderer spriteBatchRenderer) :
            base(typeof(Transform2D), typeof(Sprite))
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = componentManager.GetComponentMapper<Transform2D>();
            _sprite = componentManager.GetComponentMapper<Sprite>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var sprite = ref _sprite[entity];
                ref var transform = ref _transform[entity];

                _spriteBatchRenderer.Push(sprite.Texture, transform.Position, new Vector2(50,50), sprite.Color);
            }
        }
    }
}
