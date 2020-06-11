using System;

namespace Titan.ECS2.Components
{
    internal interface IComponentPool : IDisposable
    {
        void Destroy(uint index);
    }
}
