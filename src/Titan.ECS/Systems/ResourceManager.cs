using System;
using System.Collections.Generic;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Messaging.Messages;

namespace Titan.ECS.Systems
{
    public abstract class ResourceManager<TIdentifier, TResource> : IResourceManager
        // TODO: do we need a Dispose function?
    {
        private readonly IDictionary<TIdentifier, LoadedResource> _loadedResources = new Dictionary<TIdentifier, LoadedResource>();
        
        public void Manage(IWorld world)
        {
            // TODO: Add handling of world destruction
            var componentMap = world.GetComponentMap<Resource<TIdentifier, TResource>>();
            
            world.Subscribe((in ComponentAddedMessage<Resource<TIdentifier, TResource>> message) => OnAdded(message.EntityId, world.Id, componentMap));
            world.Subscribe((in ComponentRemovedMessage<Resource<TIdentifier, TResource>> message) => OnRemoved(message.EntityId, componentMap));
            world.Subscribe((in EntityDestroyedMessage message) => OnEntityDestroyed(message.Id, componentMap));

            //TODO:  Loop over existing entities (if any) and call OnLoaded
        }

        private void OnEntityDestroyed(uint entityId, IComponentMap<Resource<TIdentifier, TResource>> map)
        {
            if (map.Has(entityId))
            {
                OnRemoved(entityId, map);
            }
        }

        private void OnRemoved(uint entityId, IComponentMap<Resource<TIdentifier, TResource>> map)
        {
            ref var resource = ref map[entityId];
            lock(_loadedResources)
            {
                if (_loadedResources.TryGetValue(resource.Identifier, out var loadedResource))
                {
                    if (--loadedResource.References <= 0)
                    {
                        Unload(resource.Identifier, loadedResource.Value);
                        _loadedResources.Remove(resource.Identifier);
                    }
                }
            }
        }
        private void OnAdded(uint entityId, uint worldId, IComponentMap<Resource<TIdentifier, TResource>> map)
        {
            ref var resource = ref map[entityId];
            LoadedResource loadedResource;
            lock (_loadedResources)
            {
                if (_loadedResources.TryGetValue(resource.Identifier, out loadedResource))
                {
                    loadedResource.References++;
                }
                else
                {
                    loadedResource = _loadedResources[resource.Identifier] = new LoadedResource(Load(resource.Identifier));
                }
            }
            OnLoaded(new Entity(entityId, worldId), resource.Identifier, loadedResource.Value);
        }


        protected abstract TResource Load(in TIdentifier identifier);
        protected virtual void Unload(in TIdentifier identifier, TResource resource) => (resource as IDisposable)?.Dispose();
        protected abstract void OnLoaded(Entity entity, in TIdentifier identifier, TResource resource);

        private class LoadedResource
        {
            internal int References;
            internal readonly TResource Value;

            public LoadedResource(in TResource value)
            {
                Value = value;
                References = 1;
            }
        }
    }
}
