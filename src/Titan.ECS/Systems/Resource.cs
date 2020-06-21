namespace Titan.ECS.Systems
{
    public readonly struct Resource<TIdentifier, TResource>
    {
        public readonly TIdentifier Identifier;
        public Resource(TIdentifier identifier)
        {
            Identifier = identifier;
        }
    }
}
