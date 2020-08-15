using System.IO;
using System.Runtime.CompilerServices;
using Titan.Core.Assets.Wave;
using Titan.Core.Ioc;
using Titan.ECS;
using Titan.ECS.Runners;
using Titan.Xaudio2;

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

            var filename = @"F:\Git\GameDev\resources\sound\speck_-_Hydrogen_Sky0.wav";
            using var file = File.OpenRead(filename);

            using var wave = container
                .GetInstance<IWaveReader>()
                .ReadFromStream(file);


            var factory = container
                .GetInstance<IXAudioDeviceFactory>();

            using var device = factory.CreateDevice();
            using var masteringVoice = device.CreateMasteringVoice();

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
