namespace Titan.Core.GameLoop
{

    // TODO: might use this
    public readonly ref struct Timestep
    {
        public float Milliseconds { get; }
        public float Seconds => Milliseconds * 0.001f;
        public Timestep(float time)
        {
            Milliseconds = time;
        }
    }
}
