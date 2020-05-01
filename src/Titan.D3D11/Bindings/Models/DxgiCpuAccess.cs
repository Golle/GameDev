namespace Titan.D3D11.Bindings.Models
{
    internal enum DxgiCpuAccess : uint
    {
        None = 0,
        Dynamic = 1,
        ReadWrite = 2,
        Scratch = 3,
        Field = 15
    }
}
