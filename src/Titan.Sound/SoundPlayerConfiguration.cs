namespace Titan.Sound
{
    public struct SoundPlayerConfiguration
    {
        public ushort NumberOfChannels;
        public uint SamplesPerSecond;
        public uint AverageBytesPerSecond;
        public ushort BlockAlign;
        public ushort BitsPerSample;
        public int NumberOfPlayers;
    }
}
