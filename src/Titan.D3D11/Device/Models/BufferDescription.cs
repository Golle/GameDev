namespace Titan.D3D11.Device.Models
{
    public class BufferDescription
    {
        public uint ByteWidth { get; set; }
        public D3D11BufferUsage Usage { get; set; }
        public D3D11BindFlag BindFlags { get; set; }
        public D3D11CpuAccessFlag CpuAccessFlags { get; set; }
        public D3D11ResourceMiscFlag MiscFlags { get; set; }
        public uint StructureByteStride { get; set; }

    }
}