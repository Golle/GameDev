namespace Titan.Core.Assets.WavefrontObj
{
    internal readonly struct ObjLine
    {
        public ObjTypes Type { get; }
        public string Value { get; }
        public ObjLine(ObjTypes type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
