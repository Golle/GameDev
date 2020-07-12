using Titan.ECS.Serialization;

namespace Titan.ECS.Messaging.Messages
{
    internal readonly struct QueryComponentsMessage
    {
        public readonly uint EntityId;
        public readonly IComponentReader Reader;
        public QueryComponentsMessage(uint entityId, IComponentReader reader)
        {
            EntityId = entityId;
            Reader = reader;
        }
    }
}
