namespace Titan.Core.Configuration
{
    public class PlayerConfiguration
    {
        public string Identifier { get; set; }
        public int NumberOfChannels { get; set; }
        public int SamplesPerSecond { get; set; }
        public int AverageBytesPerSecond { get; set; }
        public int BlockAlign { get; set; }
        public int BitsPerSample { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}
