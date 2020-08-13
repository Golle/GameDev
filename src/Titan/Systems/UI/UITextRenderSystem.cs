using System;
using System.Net.Mail;
using System.Numerics;
using Titan.Components;
using Titan.Components.UI;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics.Renderer;
using Titan.Resources;

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


        private bool reverse;
        private bool first;


        private float width = 200f;
        protected override void OnPreUpdate()
        {
            first = true;
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var rect = ref _transform[entityId];
            var font = _font[entityId].Font;
            ref var text = ref _text[entityId];
            ref var s = ref rect.Size;

            // Stupid test code to animate the rectangle :) don't do this kids!
            if (first)
            {
                var speed = 150f * deltaTime;
                if (reverse)
                {
                    width -= speed;
                }
                else
                {
                    width += speed;
                }

                if (width > 1200)
                    reverse = true;
                if (width < 300)
                    reverse = false;

                s.Width = (int)width;
                first = false;
            }


            //TODO: This needs to be moved to the text renderer. All calculations with the fontSizeFactory should be handled at the renderer, and not here. (or when creating the Text)
            var fontSizeFactor = text.FontSize == 0 ? 1f : text.FontSize/(float)font.FontSize;

            var lineHeight = text.LineHeight != 0 ? text.LineHeight : font.LineHeight * fontSizeFactor;
            
            var maxWidth = rect.Size.Width;
            var advanceX = 0;
            var position = new Vector2(rect.WorldPosition.X, rect.WorldPosition.Y + rect.Size.Height - lineHeight); // The starting position for the text
            foreach (var letter in text.Text)
            {
                var character = font.GetCharacter(letter);
                var characterWidth = character.Size.Width * fontSizeFactor;
                var characterHeight = character.Size.Height * fontSizeFactor;

                if (advanceX + characterWidth >= maxWidth)
                {
                    position = new Vector2(position.X, position.Y - lineHeight); // Move the cursor down
                    advanceX = 0;
                }
                var offset = character.Offset * fontSizeFactor;

                var characterPosition = new Vector2(position.X + offset.X + advanceX, position.Y + offset.Y);
                _renderer.Push(font.Texture, character.TextureCoordinates, characterPosition, new Vector2(characterWidth, characterHeight), text.Color);
                advanceX += (int)((character.AdvanceX * fontSizeFactor) + 0.5f);
            }
        }

        protected override void OnPostUpdate()
        {
            _renderer.Flush();
            _renderer.Render();
        }
    }
}
