using Titan.IOC;
using Titan.Tools.AssetsBuilder.Converters.Models;
using Titan.Tools.AssetsBuilder.Converters.Textures;
using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.Files;
using Titan.Tools.AssetsBuilder.Images;
using Titan.Tools.AssetsBuilder.Logging;
using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder
{
    internal static class Bootstrapper
    {
        public static IContainer CreateContainer() =>
            new Container()
                .Register<ILogger, Logger>()

                .Register<IModelConverter, ModelConverter>()

                .Register<IModelExporter, ModelExporter>()

                .Register<IByteWriter, ByteWriter>()
                .Register<IByteReader, ByteReader>()

                .Register<ObjParser>()

                .Register<ITextureConverter, TextureConverter>()
                .Register<ITextureExporter, TextureExporter>()
                .Register<ImageLoader>()

        ;
    }
}
