using System;

namespace Titan.ECS3.Systems
{
    public interface ISystem : IDisposable
    {
        void Update(float deltaTime);
    }
}
