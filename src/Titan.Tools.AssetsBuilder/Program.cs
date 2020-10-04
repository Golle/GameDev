using System;
using System.IO;
using Titan.IOC;
using Titan.Tools.AssetsBuilder.Converters.Models;
using Titan.Tools.AssetsBuilder.Converters.Textures;
using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder
{
    internal class Program
    {
        private static readonly IContainer Container = Bootstrapper.CreateContainer();
        private static readonly string RootPath = FindRootPath(Environment.CurrentDirectory);
        private static readonly string ResourceDirectory = Path.Combine(RootPath, "resources");
        static void Main(string[] args)
        {
            
            Console.WriteLine("Select option: ");
            Console.WriteLine("1 - Models");
            Console.WriteLine("2 - Textures");
            Console.WriteLine("Default - all");

            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    ConvertModels();
                    break;
                case '2':
                    ConvertTextures();
                    break;
                default:
                    ConvertModels(); 
                    ConvertTextures(); 
                    break;
            }
        }

        private static void ConvertTextures()
        {
            var supportedFormats = new[] {"*.png", "*.jpg"};
            var texturesOutDirectory = Path.Combine(ResourceDirectory, "textures");

            if (!Directory.Exists(texturesOutDirectory))
            {
                Directory.CreateDirectory(texturesOutDirectory);
            }

            var converter = Container.GetInstance<ITextureConverter>();
            var exporter = Container.GetInstance<ITextureExporter>();
            foreach (var format in supportedFormats)
            {
                foreach (var filename in Directory.GetFiles(ResourceDirectory, format, SearchOption.AllDirectories))
                {
                    var outputFilename = Path.ChangeExtension(Path.Combine(texturesOutDirectory, Path.GetRelativePath(ResourceDirectory, filename)), ".dot");
                    var outDirectory = Path.GetDirectoryName(outputFilename) ?? throw new InvalidOperationException($"Crap, failed to read the directory from '{outputFilename}'");
                    if (!Directory.Exists(outDirectory))
                    {
                        Directory.CreateDirectory(outDirectory);
                    }
                    exporter.Export(converter.Convert(filename), outputFilename, true);
                }
            }
        }

        private static void ConvertModels()
        {
            var outDirectory = Path.Combine(ResourceDirectory, "models");
            if (!Directory.Exists(outDirectory))
            {
                Directory.CreateDirectory(outDirectory);
            }
            var converter = Container.GetInstance<IModelConverter>();
            var exporter = Container.GetInstance<IModelExporter>();
            foreach (var model in Directory.GetFiles(ResourceDirectory, "*.obj", SearchOption.AllDirectories))
            {
                var outputFileName = Path.Combine(outDirectory, Path.GetFileName(model).Replace(".obj", ".dat"));
                exporter.Write(outputFileName, converter.ConvertFromObj(model), true);
            }
        }

        private static string FindRootPath(string currentDirectory)
        {
            if (currentDirectory == null)
            {
                throw new InvalidOperationException("Well this was unexpected.");
            }
            if (currentDirectory.EndsWith("src"))
            {
                return Directory.GetParent(currentDirectory)?.ToString();
            }
            return FindRootPath(Directory.GetParent(currentDirectory)?.ToString());
        }
    }
}
