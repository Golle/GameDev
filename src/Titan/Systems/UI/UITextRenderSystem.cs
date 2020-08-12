using System;
using System.Numerics;
using Titan.Components;
using Titan.Components.UI;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;

namespace Titan.Systems.UI
{
    public struct TextVertexData
    {
        public Vector2 Position;
        public Vector2 Texture;
        public Color Color;
    }

    internal class UITextRenderSystem : EntitySystem
    {
        private readonly ISpriteBatchRenderer _renderer;

        private readonly IComponentMap<TransformRectComponent> _transform;
        private readonly IComponentMap<FontComponent> _font;
        private readonly IComponentMap<UITextComponent> _text;

        public UITextRenderSystem(IWorld world, ISpriteBatchRenderer renderer) 
            : base(world, world.EntityFilter().With<TransformRectComponent>().With<FontComponent>().With<UITextComponent>())
        {
            _renderer = renderer;
            _transform = Map<TransformRectComponent>();
            _font = Map<FontComponent>();
            _text = Map<UITextComponent>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var rect = ref _transform[entityId];
            var font = _font[entityId].Font;
            ref var text = ref _text[entityId];

            var advanceX = 0;
            foreach(var letter in text.Text)
            {
                var character = font.GetCharacter(letter);
                var position = new Vector2(rect.WorldPosition.X + advanceX, rect.WorldPosition.Y) + character.Offset;
                _renderer.Push(font.Texture, character.TextureCoordinates, position, new Vector2(character.Size.Width, character.Size.Height), text.Color);
                advanceX += character.AdvanceX;
            }
        }

        protected override void OnPostUpdate()
        {
            _renderer.Flush();
            _renderer.Render();
        }
    }
}
