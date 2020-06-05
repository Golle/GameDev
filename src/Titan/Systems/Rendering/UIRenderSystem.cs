using System.Numerics;
using Titan.Components;
using Titan.D3D11;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.Rendering
{
    internal class UIRenderSystem : BaseSystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMapper<TransformRect> _transform;
        private readonly IComponentMapper<Sprite> _sprite;

        public UIRenderSystem(IComponentManager componentManager, ISpriteBatchRenderer spriteBatchRenderer) 
            : base(typeof(TransformRect), typeof(Sprite))
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = componentManager.GetComponentMapper<TransformRect>();
            _sprite = componentManager.GetComponentMapper<Sprite>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var rect = ref _transform[entity];
                ref var sprite = ref _sprite[entity];

                _spriteBatchRenderer.Push(sprite.Texture, new Vector2(rect.Position.X, rect.Position.Y), new Vector2(rect.Size.Width, rect.Size.Height), new Color(1,0,0));
            }
        }
    }
}
