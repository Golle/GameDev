using System;
using System.Collections.Generic;

namespace Titan.ECS.Systems
{
    public abstract class BaseSystem : ISystem
    {
        private readonly ISet<uint> _addedEntities = new HashSet<uint>(100);
        private readonly ISet<uint> _removedEntities = new HashSet<uint>(100);
        private readonly ISet<uint> _entites = new HashSet<uint>(10_000);

        private bool _isDirty;
        public ulong Signature { get; }
        protected BaseSystem(params Type[] types)
        {
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

            foreach (var entity in _entites)
            {
                Update(deltaTime, entity);
            }
        }

        protected abstract void Update(float deltaTime, uint entity);

    }
}
