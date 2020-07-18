using System.Numerics;
using Titan.Components;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class UIRenderSystem : EntitySystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMap<TransformRect> _transform;
        private readonly IComponentMap<Sprite> _sprite;
        private readonly IComponentMap<Texture2D> _texture;

        public UIRenderSystem(IWorld world, ISpriteBatchRenderer spriteBatchRenderer) 
            : base( world, world.EntityFilter().With<TransformRect>()/*.With<Sprite>()*/.With<Texture2D>())
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = Map<TransformRect>();
            _sprite = Map<Sprite>();
            _texture = Map<Texture2D>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var rect = ref _transform[entityId];
            ref var sprite = ref _sprite[entityId];
            ref var texture = ref _texture[entityId];


            
            _spriteBatchRenderer.Push(texture.Texture, sprite.TextureCoordinates, new Vector2(rect.Position.X, rect.Position.Y), new Vector2(rect.Size.Width, rect.Size.Height), sprite.Color);
        }

        protected override void OnPostUpdate()
        {
            _spriteBatchRenderer.Flush();
            _spriteBatchRenderer.Render();
        }
    }
}
