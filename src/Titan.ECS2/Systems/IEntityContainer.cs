using Titan.ECS2.Components.Messages;

namespace Titan.ECS2.Systems
{
    internal interface IEntityContainer
    {
        void Add<T>(in ComponentAddedMessage<T> message) where T : struct;
        void Remove<T>(in ComponentRemovedMessage<T> message) where T : struct;
    }
}