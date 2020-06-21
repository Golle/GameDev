using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Titan.ECS.Messaging;
using Titan.ECS.Messaging.Messages;

namespace Titan.ECS.Entities
{
    internal class EntityManager : IRelationship
    {
        private readonly uint _worldId;
        private readonly Publisher _publisher;

        private readonly IdDispatcher _idDispatcher = new IdDispatcher();
        private readonly EntityInfo[] _entityInfos;
        public EntityManager(uint worldId, uint maxEntities, Publisher publisher)
        {
            _worldId = worldId;
            _publisher = publisher;
            _entityInfos = new EntityInfo[maxEntities];
        }

        public Entity Create()
        {
            var id = _idDispatcher.Next();
            // we could use entityInfo = default but that would put the heap allocated list in the GC. We don't want that. If we've allocated memory for it we wont clear it.
            ref var entityInfo = ref _entityInfos[id];
            entityInfo.Reset();

            _publisher.Publish(new EntityCreatedMessage(id));

            return new Entity(id, _worldId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Destroy(in Entity entity) => Destroy(entity.Id);

        internal void Destroy(uint entityId)
        {
            // Detach the entity from its parent
            ref var info = ref _entityInfos[entityId];
            if (info.HasParent)
            {
                var parentId = info.Parent;
                ref var parent = ref _entityInfos[parentId];
                parent.Children.Remove(entityId);
                _publisher.Publish(new EntityDetachedMessage(parentId, entityId));
            }
            RecursiveFree(entityId);
        }

        internal void RecursiveFree(uint entityId)
        {
            ref var info = ref _entityInfos[entityId];
            if (info.HasChildren)
            {
                for (var i = 0; i < info.Children.Count; ++i)
                {
                    RecursiveFree(info.Children[i]);
                }
            }
            //Console.WriteLine($"Freeing entity: {entityId}");
            _publisher.Publish(new EntityDestroyedMessage(entityId));
            _idDispatcher.Free(entityId);
        }

        // Attach a child entity to a parent
        internal void Attach(uint parentId, uint childId)
        {
            //Console.WriteLine($"Attaching {childId} to {parentId}");
            ref var parent = ref _entityInfos[parentId];
            ref var child = ref _entityInfos[childId];

            // If the entity already has a parent, detach it first.
            if (child.Parent != 0u)
            {
                Detach(childId);
            }

            child.Parent = parentId;
            (parent.Children ??= new List<uint>()).Add(childId);

            UpdateNumberOfParents(childId, parent.NumberOfParents + 1u);
            _publisher.Publish(new EntityAttachedMessage(parentId, childId));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private void UpdateNumberOfParents(uint entityId, uint numberOfParents)
        {
            ref var entityInfo = ref _entityInfos[entityId];
            entityInfo.NumberOfParents = (ushort) numberOfParents;
            if (!entityInfo.HasChildren)
            {
                return;
            }

            for (var i = 0; i < entityInfo.Children.Count; ++i)
            {
                UpdateNumberOfParents(entityInfo.Children[i], numberOfParents + 1u);
            }
        }

        // Detach a child from a parent (only the child ID is required)
        internal void Detach(uint entityId)
        {
            ref var child = ref _entityInfos[entityId];
            
            var parentId = child.Parent;
            ref var parent = ref _entityInfos[parentId];// 0 will reference the first element in the array (which wont be used by any valid entity)
            
            child.Parent = 0u;
            parent.Children?.Remove(entityId);

            UpdateNumberOfParents(entityId, 0);
            _publisher.Publish(new EntityDetachedMessage(parentId, entityId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref EntityInfo GetInfo(uint entityId) => ref _entityInfos[entityId];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetParent(uint entityId, out uint parentId)
        {
            ref var info = ref _entityInfos[entityId];
            parentId = info.Parent;
            return info.HasParent;
        }
    }
}
