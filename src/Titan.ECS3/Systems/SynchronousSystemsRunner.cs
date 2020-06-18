using System;

namespace Titan.ECS3.Systems
{
    public class SynchronousSystemsRunner : ISystemsRunner
    {
        private readonly ISystem[] _systems;
        public SynchronousSystemsRunner(ISystem[] systems)
        {
            _systems = systems ?? throw new ArgumentNullException(nameof(systems));
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < _systems.Length; ++i)
            {
                _systems[i].PreUpdate();
            }

            for (var i = 0; i < _systems.Length; ++i)
            {
                _systems[i].Update(deltaTime);
            }
        }
    }
}
