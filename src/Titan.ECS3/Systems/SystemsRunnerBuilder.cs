using System;
using System.Collections.Generic;
using System.Linq;
using Titan.Core.Ioc;

namespace Titan.ECS3.Systems
{
    public class SystemsRunnerBuilder
    {
        private readonly IContainer _container;
        private readonly IList<Type> _systems = new List<Type>();
        public SystemsRunnerBuilder(IWorld world, IContainer container)
        {
            _container = container;
            _container.RegisterSingleton<IWorld>(world);
        }

        public SystemsRunnerBuilder WithSystem<T>() where T : class, ISystem
        {
            _systems.Add(typeof(T));
            return this;
        }

        public ISystemsRunner Build()
        {
            var systems = _systems
                .Select(s => (ISystem) _container.CreateInstance(s))
                .ToArray();
            return new SynchronousSystemsRunner(systems);
        }
    }
}
