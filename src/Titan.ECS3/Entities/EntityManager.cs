using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS3.Entities
{
    internal class EntityManager
    {
        private readonly uint _worldId;
        private readonly IdDispatcher _idDispatcher = new IdDispatcher();
        private readonly EntityInfo[] _entityInfos;
        public EntityManager(uint worldId, uint maxEntities)
        {
            _worldId = worldId;
            _entityInfos = new EntityInfo[maxEntities];
        }
        public Entity Create()
        {
            var id = _idDispatcher.Next();
            // we could use entityInfo = default but that would put the heap allocated list in the GC. We don't want that. If we've allocated memory for it we wont clear it.
            ref var entityInfo = ref _entityInfos[id];
            entityInfo.Reset();

            return new Entity(id, _worldId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Destroy(in Entity entity) => Destroy(entity.Id);

        internal void Destroy(uint entityId)
        {
            //Console.WriteLine($"Destroying entity: {entityId}");
            Detach(entityId); // Only detach the first entity
            RecursiveFree(entityId);
        }

        internal void RecursiveFree(uint entityId)
        {
            ref var info = ref _entityInfos[entityId];
            if (info.HasChildren())
            {
                for (var i = 0; i < info.Children.Count; ++i)
                {
                    RecursiveFree(info.Children[i]);
                }
            }
            //Console.WriteLine($"Freeing entity: {entityId}");

            // TODO: Send Entity Destroyed message
            _idDispatcher.Free(entityId);
        }

        // Attach a child entity to a parent
        internal void Attach(uint parentId, uint childId)
        {
            //Console.WriteLine($"Attaching {childId} to {parentId}");
            ref var parent = ref _entityInfos[parentId];
            ref var child = ref _entityInfos[childId];
            
            Debug.Assert(child.Parent == 0u, "Moving entities are not supported");
            
            child.Parent = parentId;
            (parent.Children ??= new List<uint>()).Add(childId);

            // TODO: Send Entity Attached message
        }

        // Detach a child from a parent (only the child ID is required)

        internal void Detach(uint entityId)
        {
            ref var child = ref _entityInfos[entityId];
            ref var parent = ref _entityInfos[child.Parent];// 0 will reference the first element in the array (which wont be used by any valid entity)
            //Console.WriteLine($"Detaching {entityId} from {child.Parent}");
            child.Parent = 0u;
            parent.Children?.Remove(entityId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref EntityInfo GetInfo(uint entityId) => ref _entityInfos[entityId];
    }
}
