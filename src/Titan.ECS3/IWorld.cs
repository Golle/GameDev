using System;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    public interface IWorld : IDisposable
    {
        Entity CreateEntity();

        internal uint MaxEntities { get; }
    }


    internal class EntitySet
    {
        
        public EntitySet()
        {
        }
        
        public ReadOnlySpan<uint> GetEntites() => throw new NotImplementedException();
    }
}
