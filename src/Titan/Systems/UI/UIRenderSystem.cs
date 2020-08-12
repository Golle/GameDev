using System.Numerics;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.UI
{
    internal class UIRenderSystem : EntitySystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMap<TransformRectComponent> _transform;
        private readonly IComponentMap<Sprite> _sprite;
        private readonly IComponentMap<Texture2D> _texture;

        public UIRenderSystem(IWorld world, ISpriteBatchRenderer spriteBatchRenderer) 
            : base( world, world.EntityFilter().With<TransformRectComponent>().With<Sprite>().With<Texture2D>())
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = Map<TransformRectComponent>();
            _sprite = Map<Sprite>();
            _texture = Map<Texture2D>();
        }
        protected override void OnUpdate(float deltaTime, uint entityId)
        {
        
            ref var rect = ref _transform[entityId];
            ref var sprite = ref _sprite[entityId];
            ref var texture = ref _texture[entityId];

            _spriteBatchRenderer.Push(texture.Texture, sprite.TextureCoordinates, new Vector2(rect.WorldPosition.X, rect.WorldPosition.Y), new Vector2(rect.Size.Width, rect.Size.Height), sprite.Color);
        }

        protected override void OnPostUpdate()
        {
            _spriteBatchRenderer.Flush();
            _spriteBatchRenderer.Render();
        }
    }
}
