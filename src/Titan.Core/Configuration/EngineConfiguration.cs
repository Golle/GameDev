using System;
using System.IO;

namespace Titan.Core.Configuration
{
    internal class EngineConfiguration : IConfiguration
    {
        public bool Debug => true;
        public int FixedUpdateFrequency => 30; // 30 hz
        public string ResourcesPath { get; }
        public string ShadersPath { get; }
        public string FontsPath { get; }
        public string SoundsPath { get; }

        public EngineConfiguration()
        {
            var rootPath = FindRootPath(Environment.CurrentDirectory);
            ResourcesPath = Path.Combine(rootPath, "resources");
            ShadersPath = Path.Combine(ResourcesPath, "shaders");
            FontsPath = Path.Combine(ResourcesPath, "fonts");
            SoundsPath = Path.Combine(ResourcesPath, "sounds");
        }


        //TODO: This is not a good solution, but it works during development.
        public string FindRootPath(string currentDirectory)
        {
            if (currentDirectory == null)
            {
                throw new InvalidOperationException("Something went wrong.");
            }
            if (currentDirectory.EndsWith("src"))
            {
                return Directory.GetParent(currentDirectory)?.ToString();
            }
            return FindRootPath(Directory.GetParent(currentDirectory)?.ToString());
        }
    }
}
