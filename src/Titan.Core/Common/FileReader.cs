using System.IO;

namespace Titan.Core.Common
{
    internal class FileReader : IFileReader
    {
        public string ReadAsString(string filename)
        {

            return File.ReadAllText(filename);
        }
    }
}
