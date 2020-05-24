using System.Collections.Generic;

namespace Titan.ECS.Systems
{
    /// <summary>
    /// Simple systems runner that will just update all systems synchronously
    /// TODO: Create a multi threaded runner that will take into account for read/writes of components
    /// </summary>
    internal class SingleThreadSystemsRunner : ISystemsRunner
    {
        private readonly IList<ISystem> _systems = new List<ISystem>();

        public void RegisterSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _systems)
            {
                system.Update(deltaTime);
            }
        }
    }
}
