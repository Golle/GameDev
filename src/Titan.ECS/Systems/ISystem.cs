using System;

namespace Titan.ECS.Systems
{
    public interface ISystem : IDisposable
    {
        void Update(float deltaTime);
    }
}
