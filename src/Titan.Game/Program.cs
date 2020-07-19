using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Titan.Core.Ioc;
using Titan.ECS;
using Titan.ECS.Runners;

namespace Titan.Game
{
    internal class Program : Application<Program>
    {
        private GameWorld _gameWorld;

        static void Main(string[] args)
        {
            Start();
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void OnTestEvent()
        {
            //noop
        }

        protected override SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder) =>
            builder.WithSystem<PlayerControllerSystem>();

        protected override void OnInitialize(IContainer container)
        {
            _gameWorld = container.GetInstance<GameWorld>();
        }

        protected override void OnStart()
        {

        }

        protected override void OnQuit()
        {
        }

        protected override IWorld BuildWorld(IWorld world)
        {
            return _gameWorld.CreateWorld(world);
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<GameWorld>();
        }
    }
}
