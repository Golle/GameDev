using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.EntityComponentSystem.Components;

namespace Titan.EntityComponentSystem.Systems
{

    
    internal class TestSystem : ISystem
    {
        public TestSystem()
        {
            
        }

        


    }

    internal interface ISystem
    {

    }


    public readonly struct ComponentSignature
    {
        public ulong Value { get; }
        public ComponentSignature(params Type[] types)
        {
            Value = 0;
            foreach (var type in types)
            {
                Value |= type.ComponentMask();
            }
        }
    }


    internal interface IComponentMapperProvider
    {
    }

    internal abstract class BaseSystem
    {

        
    }
}
