using Titan.Core.Ioc;
using Titan.ECS.World;

namespace Titan.ECS
{
    public class ECSGlobalRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IWorldCreator, WorldCreator>()
                ;
        }
    }
}
