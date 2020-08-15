namespace Titan.Xaudio2.Bindings.Models
{
    public enum AudioStreamCategory
    {
        Other = 0,
        ForegroundOnlyMedia = 1,
//#if NTDDI_VERSION < NTDDI_WINTHRESHOLD
        BackgroundCapableMedia = 2,
//#endif
        Communications = 3,
        Alerts = 4,
        SoundEffects = 5,
        GameEffects = 6,
        GameMedia = 7,
        GameChat = 8,
        Speech = 9,
        Movie = 10,
        Media = 11
    }
}