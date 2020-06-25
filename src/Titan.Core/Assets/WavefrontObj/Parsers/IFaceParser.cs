namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    public interface IFaceParser
    {
        public Face[] Parse(string line);
    }
}
