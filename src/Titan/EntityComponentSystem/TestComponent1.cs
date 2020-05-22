using System.Numerics;
using Titan.EntityComponentSystem.Components;

namespace Titan.EntityComponentSystem
{
    internal struct TestComponent1
    {
        public Matrix4x4 Transform;
    
    }

    internal interface IComponent1
    {
    }

    internal class TestComponent2 : BaseComponent
    {
        public Matrix4x4 Transform = Matrix4x4.CreateTranslation(3,4, 23);
        public override void Reset()
        {
            // noop
        }
    }
}
