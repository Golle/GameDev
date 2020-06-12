using System;

namespace Titan.ECS2.Systems
{
    internal interface ISystem : IDisposable
    {
        bool IsEnabled { get; }
        void Update(float deltaTime);
    }
}