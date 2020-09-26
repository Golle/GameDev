using Titan.IOC;
using Titan.Tools.AssetsBuilder.Logging;

namespace Titan.Tools.AssetsBuilder
{
    internal static class Bootstrapper
    {
        public static IContainer CreateContainer() =>
            new Container()
                .Register<ILogger, Logger>()

        ;
    }
}
