using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11.Bindings.Models;

namespace Titan.Graphics.Layout
{
    public class VertexLayout
    {
        public ref readonly D3D11InputElementDesc[] Descriptors => ref _descriptors;
        public uint NumberOfDescriptors { get; private set; }

        private readonly D3D11InputElementDesc[] _descriptors;
        private uint _size;
        public VertexLayout(uint numberOfDescriptors = 5)
        {
            _descriptors = new D3D11InputElementDesc[numberOfDescriptors];
        }

        public VertexLayout Append(string name, VertexLayoutTypes type)
        {
            ref var desc = ref _descriptors[NumberOfDescriptors++];
            desc.SemanticName = name;
            desc.InstanceDataStepRate = 0;// not sure about this one
            desc.InputSlot = 0;     // not sure about this one
            desc.SemanticIndex = 0; // not sure about this one
            desc.AlignedByteOffset = _size;
            desc.Format = GetFormat(type);

            _size += GetSize(type);
            return this;
        }

        private static DxgiFormat GetFormat(VertexLayoutTypes type)
        {
            switch (type)
            {
                case VertexLayoutTypes.Position2D:
                case VertexLayoutTypes.Texture2D:
                    return DxgiFormat.R32G32Float;
                case VertexLayoutTypes.Position3D:
                case VertexLayoutTypes.Normal:
                case VertexLayoutTypes.Float3Color:
                    return DxgiFormat.R32G32B32Float;
                case VertexLayoutTypes.Float4Color:
                    return DxgiFormat.R32G32B32A32Float;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static uint GetSize(VertexLayoutTypes type)
        {
            switch (type)
            {
                case VertexLayoutTypes.Position2D:
                case VertexLayoutTypes.Texture2D:
                    return (uint)Marshal.SizeOf<Vector2>();
                case VertexLayoutTypes.Position3D:
                case VertexLayoutTypes.Normal:
                case VertexLayoutTypes.Float3Color:
                    return (uint)Marshal.SizeOf<Vector3>();
                case VertexLayoutTypes.Float4Color:
                    return (uint) Marshal.SizeOf<Vector4>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public ref readonly D3D11InputElementDesc[] ToArray()
        {
            return ref _descriptors;
        }
    }
}
