using System;
using System.Numerics;
using Titan.Components;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Game
{
    public class PlayerControllerSystem : EntitySystem
    {
        private readonly IInputManager _inputManager;
        private readonly IWindow _window;
        private readonly IComponentMap<Transform3D> _transform;

        public PlayerControllerSystem(IWorld world, IInputManager inputManager, IWindow window) : base(world, world.EntityFilter(1).With<Player>().With<Transform3D>())
        {
            _inputManager = inputManager;
            _window = window;
            _transform = Map<Transform3D>();
        }

        public Vector3 _rotation;

        private bool _mouseVisible = true;
        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            const float speed = 12f;
            const float maxRotation = (float)Math.PI / 2f - 0.01f;

            if (_inputManager.Keyboard.IsKeyReleased(KeyCode.Space))
            {
                _mouseVisible = !_mouseVisible;
                _window.ShowCursor(_mouseVisible);
            }

            if (_mouseVisible)
            {
                return;
            }

            var multiplier = _inputManager.Keyboard.IsKeyDown(KeyCode.Shift) ? 3f : 1f;
            var distance = speed * deltaTime * multiplier;
            ref var transform = ref _transform[entityId];
            
            var keyboard = _inputManager.Keyboard;
            var movement = Vector3.Zero;
            if (keyboard.IsKeyDown(KeyCode.W))
            {
                movement.Z -= distance;
            }
            if (keyboard.IsKeyDown(KeyCode.S))
            {
                movement.Z += distance;
            }
            if (keyboard.IsKeyDown(KeyCode.A))
            {
                movement.X += distance;
            }
            if (keyboard.IsKeyDown(KeyCode.D))
            {
                movement.X -= distance;
            }
            
            var mousePos = _inputManager.Mouse.DeltaMovement;
            if (mousePos.Y != 0 || mousePos.X != 0)
            {
                const float constant = 0.003f;
                _rotation.X -= mousePos.X * constant;
                _rotation.Y = Math.Clamp(_rotation.Y + mousePos.Y * constant, -maxRotation, maxRotation);

                transform.Rotation = Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, 0);
            }
            movement = Vector3.Transform(movement, transform.Rotation);
            
            transform.Position += movement;
        }

        protected override void OnPostUpdate()
        {

            // Temporary to support exiting the app
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.Escape))
            {
                Environment.Exit(0);
            }
        }
    }
}