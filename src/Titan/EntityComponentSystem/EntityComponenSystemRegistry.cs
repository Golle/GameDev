using Titan.Core.Ioc;
using Titan.EntityComponentSystem.Components;
using Titan.EntityComponentSystem.Entities;

//using Titan.EntityComponentSystem.Components;
//using Titan.EntityComponentSystem.Configuration;
//using Titan.EntityComponentSystem.Entities;

namespace Titan.EntityComponentSystem
{
    internal class EntityComponenSystemRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                //.Register<IEntityComponentSystemConfiguration, EntityComponentSystemConfiguration>()
                .Register<IEntityManager, EntityManager>()
                .Register<IComponentManager, ComponentManager>()
                //.Register<IComponentRegister, ComponentRegister>()
                ;
        }
    }
}
