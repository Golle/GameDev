using System;
using System.IO;

namespace Titan.Core.Configuration
{
    internal class EnginePaths : IEnginePaths
    {
        public string ConfigurationPath { get; }
        public string ResourcesPath { get; }
        public EnginePaths()
        {
            var rootPath = FindRootPath(Environment.CurrentDirectory);
            ResourcesPath = Path.Combine(rootPath, "resources");
            ConfigurationPath = Path.Combine(ResourcesPath, "config");
        }

        private string FindRootPath(string currentDirectory)
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
