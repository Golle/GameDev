using Titan.ECS3.Components;
using Titan.ECS3.Entities;
using Titan.ECS3.Messaging;

namespace Titan.ECS3
{
    internal class World : IWorld
    {
        internal uint Id { get; }
        internal EntityManager EntityManager { get; }
        internal ComponentManager ComponentManager { get; }
        internal Publisher Publisher { get; }

        private readonly uint _maxEntities;
        uint IWorld.MaxEntities => _maxEntities;

        internal World(WorldConfiguration configuration)
        {
            Id = Worlds.AddWorld(this);
            _maxEntities = configuration.MaxEntities;
            
            Publisher = new Publisher(Id);

            EntityManager = new EntityManager(Id, configuration.MaxEntities, Publisher);

            ComponentManager = new ComponentManager(configuration.MaxEntities);
            foreach (var (componentType, size) in configuration.Components())
            {
                ComponentManager.RegisterComponent(componentType, size);
            }
        }

        public Entity CreateEntity()
        {
            var entity = EntityManager.Create();
            return entity;
        }

        public void Dispose()
        {
            Worlds.DestroyWorld(this);
        }
    }
}
