using System;
using System.Collections.Generic;
using Titan.ECS2.Components;
using Titan.ECS2.Components.Messages;

namespace Titan.ECS2.Systems
{
    public class EntitySetBuilder
    {
        private readonly uint _maxEntities;
        private readonly IPublisher _publisher;
        private ComponentSignature _withFilter;

        private readonly IList<Func<IPublisher, EntitySet, IDisposable>> _subscriptions = new List<Func<IPublisher, EntitySet, IDisposable>>();

        internal EntitySetBuilder(uint maxEntities, IPublisher publisher)
        {
            _maxEntities = maxEntities;
            _publisher = publisher;

            //_subscriptions.Add(); // add Entity created and destroyed subscriptions (maybe just destoryed)
        }

        public EntitySetBuilder With<T>() where T : struct
        {
            var id = ComponentId<T>.Id;
            if (!_withFilter.Contains(id))
            {
                _withFilter.Add(id);
                _subscriptions.Add((publisher, entitySet) => publisher.Subscribe<ComponentAddedMessage<T>>(entitySet.AddChecked));
                _subscriptions.Add((publisher, entitySet) => publisher.Subscribe<ComponentRemovedMessage<T>>(entitySet.RemoveChecked));
            }

            return this;
        }

        public EntitySet Build()
        {
            return new EntitySet(_publisher, _maxEntities, _withFilter, _subscriptions);
        }
    }
}