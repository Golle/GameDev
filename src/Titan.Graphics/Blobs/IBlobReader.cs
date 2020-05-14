namespace Titan.Graphics.Blobs
{
    public interface IBlobReader
    {
        IBlob ReadFromFile(string fileName);
    }
}
