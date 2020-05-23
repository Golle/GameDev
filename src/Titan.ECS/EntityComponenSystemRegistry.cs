using Titan.Core.Ioc;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;

namespace Titan.ECS
{
    public class EntityComponenSystemRegistry : IRegistry
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
