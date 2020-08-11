using System.IO;

namespace Titan.Core.Assets.Angelfont
{
    public interface IAngelfontParser
    {
        Angelfont ParseFromStream(StreamReader reader);
    }
}
