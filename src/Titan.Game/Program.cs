using System;
using System.Numerics;
using Titan.Components;
using Titan.Core.Ioc;
using Titan.ECS;
using Titan.ECS.Components;
using Titan.ECS.Runners;
using Titan.ECS.Systems;
using Titan.Windows;
using Titan.Windows.Input;

namespace Titan.Game
{

    

    public class PlayerControllerSystem : EntitySystem
    {
        private readonly IInputManager _inputManager;
        private readonly IComponentMap<Transform3D> _transform;

        public PlayerControllerSystem(IWorld world, IInputManager inputManager) : base(world, world.EntityFilter(1).With<Player>().With<Transform3D>())
        {
            _inputManager = inputManager;
            _transform = Map<Transform3D>();
        }


        public Vector3 _rotation;
        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            const float speed = 12f;
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
                _rotation.Y += mousePos.Y * constant;
                transform.Rotation = Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, 0);
            }
            movement = Vector3.Transform(movement, transform.Rotation);
            
            transform.Position += movement;
        }

        protected override void OnPostUpdate()
        {
            
        }
    }
    internal class Program : Application<Program>
    {
        private MyTestClass _test;

        static void Main(string[] args)
        {
            Start();
        }

        protected override SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder) =>
            builder.WithSystem<PlayerControllerSystem>();

        protected override void OnInitialize(IContainer container)
        {
            _test = container.GetInstance<MyTestClass>();
        }

        protected override void OnStart()
        {
            _test.OnStart();
        }

        protected override void OnQuit()
        {
            _test.OnQuit();
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<MyTestClass>();
        }
    }

    internal class MyTestClass
    {
        public void OnStart()
        {
            
            

            Console.WriteLine("OnStart");
        }

        public void OnQuit()
        {
            Console.WriteLine("OnQuit");
        }
    }
}
