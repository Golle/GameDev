using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Titan.ECS.Systems
{
    public abstract class BaseSystem : ISystem
    {
        private readonly ISet<uint> _addedEntities = new HashSet<uint>(100);
        private readonly ISet<uint> _removedEntities = new HashSet<uint>(100);
        private readonly ISet<uint> _entites = new HashSet<uint>(10_000);

        protected IEnumerable<uint> Entities => _entites;

        private bool _isDirty;
        public ulong Signature { get; }
        protected BaseSystem(params Type[] types)
        {
            Debug.Assert(types.Length > 0, $"No component types have been registered for system {GetType().Name}");
            Signature = new ComponentSignature(types).Value;
        }

        public void Remove(uint entity)
        {
            _removedEntities.Add(entity);
            _isDirty = true;
        }

        public void Add(uint entity)
        {
            _addedEntities.Add(entity);
            _isDirty = true;
        }

        public bool IsMatch(ulong signature) => (signature & Signature) == Signature;

        public void Update(float deltaTime)
        {
            if (_isDirty)
            {
                foreach (var entity in _addedEntities)
                {
                    _entites.Add(entity);
                }
                foreach (var entity in _removedEntities)
                {
                    _entites.Remove(entity);
                }
                _addedEntities.Clear();
                _removedEntities.Clear();
                _isDirty = false;
            }
            OnUpdate(deltaTime);
        }

        protected abstract void OnUpdate(float deltaTime);

    }
}
