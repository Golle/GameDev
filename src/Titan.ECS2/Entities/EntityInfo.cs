using System.Runtime.InteropServices;
using Titan.ECS2.Components;

namespace Titan.ECS2.Entities
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct EntityInfo
    {

        public ComponentSignature Signature;
    }
}
