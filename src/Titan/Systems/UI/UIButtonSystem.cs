using System;
using Titan.Components;
using Titan.Components.UI;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Windows.Input;
using Titan.Windows.Win32;

namespace Titan.Systems.UI
{
    internal class UIButtonSystem : EntitySystem
    {
        private readonly IInputManager _inputManager;
        private readonly IComponentMap<Button> _button;
        private readonly IComponentMap<TransformRect> _transform;
        
        private Point _position;
        private Point _lastPosition;

        public UIButtonSystem(IWorld world, IInputManager inputManager) : base(world, world.EntityFilter().With<Button>().With<TransformRect>())
        {
            _inputManager = inputManager;
            _transform = Map<TransformRect>();
            _button = Map<Button>();
        }

        protected override void OnPreUpdate()
        {
            var mouse = _inputManager.Mouse;
            _position = mouse.Position;
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            ref var transform = ref _transform[entityId];
            
        }

        protected override void OnPostUpdate()
        {
            _lastPosition = _position;
        }
    }
}
