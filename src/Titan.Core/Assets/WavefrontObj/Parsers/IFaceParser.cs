namespace Titan.Core.Assets.WavefrontObj.Parsers
{
    public interface IFaceParser
    {
        public FaceElement[] Parse(string line);
    }
}
