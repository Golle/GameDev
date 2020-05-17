using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;
using Titan.Core.Logging;
using Titan.Systems.Components.Events;

namespace Titan.Systems.TransformSystem
{
    internal class Transform3DSystem : ITransform3DSystem
    {
        private readonly ILogger _logger;
        private readonly Transform3D[] _transforms = new Transform3D[10_000];

        private int _numberOfTransforms = 0;

        public Transform3DSystem(IEventManager eventManager, ILogger logger)
        {
            _logger = logger;
            eventManager.Subscribe<UpdateEvent>(OnUpdate);
            eventManager.Subscribe<ComponentAddedEvent>(OnComponentAdded);
            eventManager.Subscribe<ComponentEnabledEvent>(OnComponentEnabled);
            eventManager.Subscribe<ComponentDisabledEvent>(OnComponentDisabled);
            eventManager.Subscribe<ComponentRemovedEvent>(OnComponentRemoved);
        }

        private void OnComponentRemoved(in ComponentRemovedEvent @event)
        {
            _logger.Debug("ComponentRemovedEvent");
        }

        private void OnComponentDisabled(in ComponentDisabledEvent @event)
        {
            _logger.Debug("OnComponentDisabled");
        }

        private void OnComponentEnabled(in ComponentEnabledEvent @event)
        {
            _logger.Debug("OnComponentEnabled");
        }


        private void OnComponentAdded(in ComponentAddedEvent @event)
        {
            _logger.Debug("OnComponentAdded");
        }

        private void OnUpdate(in UpdateEvent @event)
        {
            for (var i = 0; i < _numberOfTransforms; ++i)
            {
                _transforms[i].Update();
            }
        }
    }
}
