using System;
using System.IO;
using Titan.IOC;
using Titan.Tools.AssetsBuilder.Converters;
using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder
{
    
    
    internal class Program
    {
        private static readonly IContainer Container = Bootstrapper.CreateContainer();
        static void Main(string[] args)
        {
            var rootPath = FindRootPath(Environment.CurrentDirectory);
            var resourceDirectory = Path.Combine(rootPath, "resources");
            var outdirectory = Path.Combine(resourceDirectory, "models");

            if (!Directory.Exists(outdirectory))
            {
                Directory.CreateDirectory(outdirectory);
            }
            
            var converter = Container.GetInstance<IModelConverter>();
            var exporter = Container.GetInstance<IModelExporter>();
            foreach (var model in Directory.GetFiles(resourceDirectory, "*.obj", SearchOption.AllDirectories))
            {
                var outputFileName = Path.Combine(outdirectory, Path.GetFileName(model).Replace(".obj", ".dat"));
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
