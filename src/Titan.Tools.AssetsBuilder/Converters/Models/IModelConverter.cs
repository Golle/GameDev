namespace Titan.Tools.AssetsBuilder.Converters.Models
{
    internal interface IModelConverter
    {
        Mesh ConvertFromObj(string filename);
    }
}
