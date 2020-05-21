using System.Numerics;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem
{
    internal class TestComponent1 : IComponent
    {
        public Matrix4x4 Transform = Matrix4x4.CreateTranslation(1,2,3);
        public void Reset()
        {
            // noop
        }
    }

    internal class TestComponent2 : IComponent
    {
        public Matrix4x4 Transform = Matrix4x4.CreateTranslation(3,4, 23);
        public void Reset()
        {
            // noop
        }
    }
}
