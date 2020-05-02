using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings.Models
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct D3D11InputElementDesc
    {
        //[MarshalAs(UnmanagedType.LPStr)]
        public string SemanticName;
        public uint SemanticIndex;
        public DxgiFormat Format;
        public uint InputSlot;
        public uint AlignedByteOffset;
        public D3D11InputClassification InputSlotClass;
        public uint InstanceDataStepRate;
    }
}
