using System;
using System.Numerics;
using Titan.Components;
using Titan.Components.UI;
using Titan.D3D11;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;

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
        private IComponentMap<TransformRectComponent> _transform;
        private IComponentMap<FontComponent> _font;
        private IComponentMap<UITextComponent> _text;

        public UITextRenderSystem(IWorld world) 
            : base(world, world.EntityFilter().With<TransformRectComponent>().With<FontComponent>().With<UITextComponent>())
        {
            _transform = Map<TransformRectComponent>();
            _font = Map<FontComponent>();
            _text = Map<UITextComponent>();
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            
        }
    }
}
