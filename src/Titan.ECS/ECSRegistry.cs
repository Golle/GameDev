using Titan.Core.Ioc;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.ECS.World;

namespace Titan.ECS
{
    public class ECSRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IEntityManager, EntityManager>()
                .Register<IComponentManager, ComponentManager>()
                .Register<IContext, Context>()
                .Register<IWorld, World.World>()
                .Register<ISystemsRunner, SingleThreadSystemsRunner>()
                ;
        }
    }
}
