using System;

namespace Titan.ECS3.Systems
{
    /// <summary>
    /// Base system that does logic on a set of Components (entities)
    /// </summary>
    public abstract class EntitySystem : ISystem
    {
        protected EntitySystem(IWorld world)
        {
            
        }
        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
