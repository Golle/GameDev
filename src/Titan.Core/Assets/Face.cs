namespace Titan.Core.Assets
{
    public readonly struct Face
    {
        public readonly FaceElement[] Elements;
        public Face(FaceElement[] elements)
        {
            Elements = elements;
        }
    }
}
