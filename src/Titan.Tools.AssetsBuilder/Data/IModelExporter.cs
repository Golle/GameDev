using Titan.Tools.AssetsBuilder.Converters;

namespace Titan.Tools.AssetsBuilder.Data
{
    internal interface IModelExporter
    {
        void Write(string filename, in Mesh[] meshes, bool overwrite);
    }
}
