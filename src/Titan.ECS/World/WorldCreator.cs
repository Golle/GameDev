using System.Linq;
using Titan.ECS.Components;
using Titan.ECS.Systems;

namespace Titan.ECS.World
{
    internal class WorldCreator : IWorldCreator
    {
        public IWorld CreateWorld(WorldConfiguration configuration)
        {
            // register ECS classes in the child container
            var container = configuration
                .Container
                .AddRegistry<ECSRegistry>();


            // Create component mappers, pools and base internal components
            var componentManager = container
                .GetInstance<IComponentManager>()
                .RegisterComponent<Relationship>(configuration.MaxNumberOfEntities);

            foreach (var component in configuration.Components)
            {
                componentManager.RegisterComponent(component.Type, component.Capacity);
            }
            
            // Register systems with runner and context
            var context = container.GetInstance<IContext>();
            var runner = container.GetInstance<ISystemsRunner>();
            foreach (var system in container.GetAll<ISystem>().ToArray())
            {
                context.RegisterSystem(system);
                runner.RegisterSystem(system);
            }


            // Create the world instance
            return container
                .CreateInstance<IWorld>();
        }
    }
}
