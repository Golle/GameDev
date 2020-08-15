using System.IO;

namespace Titan.Core.Assets.Wave
{
    public interface IWaveReader
    {
        WaveData ReadFromStream(Stream stream);
    }
}
