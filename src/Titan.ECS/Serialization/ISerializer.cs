using System;
using Titan.ECS.Components;
using Titan.ECS.Messaging;
using Titan.ECS.Messaging.Messages;

namespace Titan.ECS.Serialization
{
    internal class EntitySerializer : ISerializer, IComponentReader
    {
        private readonly Publisher _publisher;
        private readonly EntityFilter _entities;
        public EntitySerializer(IWorld world, Publisher publisher)
        {
            _publisher = publisher;
            _entities = world.EntityFilter().WithAllEntities();
        }

        public void Serialize()
        {
            foreach (ref readonly var entity in _entities.GetEntities())
            {
                Console.WriteLine("Entity: {0}", entity);
                _publisher.Publish(new QueryComponentsMessage(entity, this));
            }
        }

        public void OnRead<T>(in T component) where T : struct
        {
            Console.WriteLine("Component Type: " + typeof(T).Name);
        }
    }

    internal interface IComponentReader
    {
        void OnRead<T>(in T component) where T : struct;
    }


    internal interface ISerializer
    {
        void Serialize();
    }

    internal struct EntityWriter
    {

    }
}
