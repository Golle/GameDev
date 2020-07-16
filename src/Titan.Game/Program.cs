using Titan.Core.Ioc;
using Titan.ECS.Runners;

namespace Titan.Game
{
    internal class Program : Application<Program>
    {
        static void Main(string[] args)
        {
            Start();
        }

        protected override SystemsRunnerBuilder ConfigureSystems(SystemsRunnerBuilder builder) =>
            builder.WithSystem<PlayerControllerSystem>();

        protected override void OnInitialize(IContainer container)
        {
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnQuit()
        {
        }

        protected override void RegisterServices(IContainer container)
        {
        }
    }
}
