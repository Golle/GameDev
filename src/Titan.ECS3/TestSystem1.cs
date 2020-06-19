using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.ECS3.Systems;

namespace Titan.ECS3
{
    public class TestSystem1 : EntitySystem
    {
        public TestSystem1(IWorld world) 
            : base(world, world.EntityFilter().With<Transform2D>())
        {
        }

        protected override void OnUpdate(float deltaTime, uint entityId)
        {
            Console.WriteLine("Updating entity");
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Transform2D
    {
        public Vector2 Position;

        public Vector2 WorldPosition;
    }

}
