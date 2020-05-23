using Titan.Core.Ioc;
using Titan.EntityComponentSystem.Components;
using Titan.EntityComponentSystem.Entities;
using Titan.EntityComponentSystem.Systems;

namespace Titan.EntityComponentSystem
{
    internal class EntityComponenSystemRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IEntityManager, EntityManager>()
                .Register<IComponentManager, ComponentManager>()
                .Register<IContext, Context>()
                ;
        }
    }
}
