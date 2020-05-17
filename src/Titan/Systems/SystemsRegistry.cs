using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Core.Ioc;
using Titan.Systems.Components;
using Titan.Systems.EntitySystem;
using Titan.Systems.TransformSystem;

namespace Titan.Systems
{
    class SystemsRegistry : IRegistry
    {
        public void Register(IContainer container)
        {

            container
                .Register<IEntityManager, EntityManager>()
                .Register<ITransform3DSystem, Transform3DSystem>()
                .Register<ITransform3DPool, Transform3DPool>()
                .Register<IComponentSystem, ComponentSystem>()

                ;

        }
    }
}
