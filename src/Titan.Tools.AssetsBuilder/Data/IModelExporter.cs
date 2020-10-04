using Titan.Tools.AssetsBuilder.Converters;
using Titan.Tools.AssetsBuilder.Converters.Models;

namespace Titan.Tools.AssetsBuilder.Data
{
    internal interface IModelExporter
    {
        void Write(string filename, in Mesh mesh, bool overwrite);
    }
}
