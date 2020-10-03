namespace Titan.Tools.AssetsBuilder.Converters
{
    internal interface IModelConverter
    {
        Mesh ConvertFromObj(string filename);
    }
}
