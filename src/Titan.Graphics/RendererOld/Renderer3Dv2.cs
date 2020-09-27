using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.D3D11.Compiler;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Models;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.RendererOld
{
    // Make sure this is 16 byte aligned
    [StructLayout(LayoutKind.Explicit, Size = 144)]
    internal struct LightsConstantBuffer
    {
        public const int MaxLights = 4;
        [FieldOffset(0)] // 16 bytes alignment (float4)
        public unsafe fixed float LightPositions[MaxLights * 4];
        
        [FieldOffset(64)] // 16 bytes alignment (float4)
        public unsafe fixed float LightColors[MaxLights * 4];

        [FieldOffset(128)] 
        public uint NumberOfLights;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLightPosition(uint index, in Vector3 position)
        {
            Debug.Assert(index < MaxLights, $"Index is out of range. Max lights: {MaxLights}");
            var parameter = index * 4;
            unsafe
            {
                LightPositions[parameter] = position.X;
                LightPositions[parameter + 1] = position.Y;
                LightPositions[parameter + 2] = position.Z;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLightColor(uint index, in Color color)
        {
            var parameter = index * 4;
            unsafe
            {
                LightColors[parameter] = color.R;
                LightColors[parameter + 1] = color.G;
                LightColors[parameter + 2] = color.B;
                //LightColors[parameter + 4] = color.Alpha;
            }
        }
    }


    [StructLayout(LayoutKind.Explicit, Size = 256)]
    internal struct PerFrameContantBuffer
    {
        [FieldOffset(0)]
        public Matrix4x4 View;
        [FieldOffset(64)]
        public Matrix4x4 ViewProjection;
    }

    [StructLayout(LayoutKind.Explicit, Size = 64)]
    internal struct PerObjectContantBuffer
    {
        [FieldOffset(0)]
        public Matrix4x4 World;
    }


    public class Renderer3Dv2 : IDisposable //: IRendererV2
    {
        public const uint PerFrameSlot = 0;
        public const uint PerObjectSlot = 1;

        private readonly IDevice _device;
        private readonly IConstantBuffer<PerFrameContantBuffer> _perFrameConstantBuffer;
        private readonly IConstantBuffer<LightsConstantBuffer> _lightsConstantBuffer;
        private readonly IConstantBuffer<PerObjectContantBuffer> _perObjectConstantBuffer;
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;

        private readonly ISampler _sampler;

        private readonly List<(Vector3 position, Color color)> _lights = new List<(Vector3 position, Color color)>();

        public Renderer3Dv2(IDevice device, IBlobReader blobReader, ID3DCompiler d3DCompiler)
        {
            _device = device;

            using var blob = d3DCompiler.CompileShaderFromFile(@"F:\Git\GameDev\src\Titan.D3D11.Bindings\VertexShader.hlsl", "main", "vs_5_0");
            //D3D11CommonBindings.D3DWriteBlobToFile_(blob.Handle, @"c:\tmp\cache\vs.cso", true);

            using var vertexShaderBlob = new Blob(blob);
            //using var vertexShaderBlob = blobReader.ReadFromFile(@"C:\tmp\cache\vs.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(3).Append("Position", VertexLayoutTypes.Position3D).Append("Normal", VertexLayoutTypes.Position3D).Append("Texture", VertexLayoutTypes.Texture2D), vertexShaderBlob);
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _lightsConstantBuffer = device.CreateConstantBuffer<LightsConstantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _sampler = device.CreateSampler();
        }

        public void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            _device.ImmediateContext.MapResource(_perFrameConstantBuffer, new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            _device.ImmediateContext.SetVertexShaderConstantBuffer(_perFrameConstantBuffer, PerFrameSlot);
        }

        public void Begin()
        {
            LightsConstantBuffer lights;
            lights.NumberOfLights = Math.Min((uint)_lights.Count, LightsConstantBuffer.MaxLights);
            for (var i = 0u; i < lights.NumberOfLights; ++i)
            {
                var (position, color) = _lights[(int) i];
                lights.SetLightColor(i, color);
                lights.SetLightPosition(i, position);
            }
            _device.ImmediateContext.MapResource(_lightsConstantBuffer, lights);
            _device.ImmediateContext.SetPixelShaderConstantBuffer(_lightsConstantBuffer);

            _device.ImmediateContext.SetPixelShaderSampler(_sampler);
            _device.ImmediateContext.SetInputLayout(_inputLayout);

            _device.ImmediateContext.SetVertexShader(_vertexShader);
            _device.ImmediateContext.SetPixelShader(_pixelShader);
        }


        private IVertexBuffer _lastVertexBuffer = null;
        private ITexture2D _lastTexture = null;

        public void Render(IMesh mesh, in Matrix4x4 worldMatrix, ITexture2D texture)
        {
            _device.ImmediateContext.MapResource(_perObjectConstantBuffer, new PerObjectContantBuffer { World = Matrix4x4.Transpose(worldMatrix) });
            _device.ImmediateContext.SetVertexShaderConstantBuffer(_perObjectConstantBuffer, PerObjectSlot);
            
            _device.ImmediateContext.SetPixelShaderResource(texture);
              _device.ImmediateContext.SetIndexBuffer(mesh.IndexBuffer);

            if (mesh.VertexBuffer != _lastVertexBuffer)
            {
                _device.ImmediateContext.SetVertexBuffer(mesh.VertexBuffer);
                _lastVertexBuffer = mesh.VertexBuffer;
            }

            if (_lastTexture != texture)
            {
                _device.ImmediateContext.SetPixelShaderResource(texture);
                _lastTexture = texture;
            }


            //_device.ImmediateContext.Draw(mesh.VertexBuffer.NumberOfVertices, 0);
            _device.ImmediateContext.DrawIndexed(mesh.IndexBuffer.NumberOfIndices, 0, 0);
        }

        public void End()
        {
            // noop
            _lights.Clear();
            _lastVertexBuffer = null;
            _lastTexture = null;
        }

        public void Dispose()
        {
            _perObjectConstantBuffer?.Dispose();
            _perFrameConstantBuffer?.Dispose();
            _lightsConstantBuffer?.Dispose();
            _vertexShader?.Dispose();
            _pixelShader?.Dispose();
            _inputLayout?.Dispose();
            _sampler?.Dispose();
        }

        public void SubmitLight(in Color color, in Vector3 position)
        {
            _lights.Add((position, color));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPrimitiveTopology(PrimitiveTopology topology) => _device.ImmediateContext.SetPrimitiveTopology(topology);
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct LineVertex
    {
        public Vector3 Position;
        public Color Color;

        public LineVertex(float x, float y, float z, in Color color)
        {
            Position = new Vector3(x, y, z);
            Color = color;
        }
    }

    public class RendererDebug3Dv3 : IDisposable
    {
        private readonly IDevice _device;

        private readonly IConstantBuffer<PerObjectContantBuffer> _perObjectConstantBuffer;
        private readonly IConstantBuffer<PerFrameContantBuffer> _perFrameConstantBuffer;
        private readonly IInputLayout _inputLayout;
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IIndexBuffer _indexBuffer;
        private readonly IVertexBuffer<LineVertex> _vertexBuffer;

        private const int MaxBoxes = 8000;


        private readonly LineVertex[] _vertices = new LineVertex[8 * MaxBoxes];
        private readonly short[] _indices = new short[24 * MaxBoxes];
        private uint _numberOfVertices;
        private uint _numberOfIndices;
        private readonly IDeviceContext _context;

        public RendererDebug3Dv3(IDevice device, IBlobReader blobReader)
        {
            _device = device;
            _context = device.ImmediateContext; //.CreateDeferredContext();
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/LineVertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/LinePixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("Position", VertexLayoutTypes.Position3D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);

            _indexBuffer = _device.CreateIndexBuffer((uint) _indices.Length, BufferUsage.Dynamic, BufferAccessFlags.Write);
            _vertexBuffer = _device.CreateVertexBuffer<LineVertex>((uint) _vertices.Length, BufferUsage.Dynamic, BufferAccessFlags.Write);
        }

        public void DrawBox(in Vector3 min, in Vector3 max, in Color color)
        {
            var offset = (short)_numberOfVertices;

            _vertices[_numberOfVertices++] = new LineVertex(min.X, min.Y, min.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(max.X, min.Y, min.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(min.X, max.Y, min.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(min.X, min.Y, max.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(max.X, max.Y, max.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(min.X, max.Y, max.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(max.X, min.Y, max.Z, color);
            _vertices[_numberOfVertices++] = new LineVertex(max.X, max.Y, min.Z, color);


            _indices[_numberOfIndices++] = offset;
            _indices[_numberOfIndices++] = (short)(offset + 1);
            _indices[_numberOfIndices++] = offset;
            _indices[_numberOfIndices++] = (short)(offset + 2);
            _indices[_numberOfIndices++] = offset;
            _indices[_numberOfIndices++] = (short)(offset + 3);
            _indices[_numberOfIndices++] = (short)(offset + 4);
            _indices[_numberOfIndices++] = (short)(offset + 5);
            _indices[_numberOfIndices++] = (short)(offset + 4);
            _indices[_numberOfIndices++] = (short)(offset + 6);
            _indices[_numberOfIndices++] = (short)(offset + 4);
            _indices[_numberOfIndices++] = (short)(offset + 7);
            _indices[_numberOfIndices++] = (short)(offset + 5);
            _indices[_numberOfIndices++] = (short)(offset + 2);
            _indices[_numberOfIndices++] = (short)(offset + 5);
            _indices[_numberOfIndices++] = (short)(offset + 3);
            _indices[_numberOfIndices++] = (short)(offset + 7);
            _indices[_numberOfIndices++] = (short)(offset + 2);
            _indices[_numberOfIndices++] = (short)(offset + 7);
            _indices[_numberOfIndices++] = (short)(offset + 1);
            _indices[_numberOfIndices++] = (short)(offset + 6);
            _indices[_numberOfIndices++] = (short)(offset + 3);
            _indices[_numberOfIndices++] = (short)(offset + 6);
            _indices[_numberOfIndices++] = (short)(offset + 1);
        }

        public void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            _context.MapResource(_perFrameConstantBuffer, new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            
        }

        public void Render()
        {
            _context.SetVertexShaderConstantBuffer(_perFrameConstantBuffer);
            _context.SetPrimitiveTopology(PrimitiveTopology.LineList);
            _context.MapResource(_vertexBuffer, _vertices, _numberOfVertices);
            _context.MapResource(_indexBuffer, _indices, _numberOfIndices);

            _context.SetInputLayout(_inputLayout);
            _context.SetVertexShader(_vertexShader);
            _context.SetPixelShader(_pixelShader);
            _context.SetVertexBuffer(_vertexBuffer);
            _context.SetIndexBuffer(_indexBuffer);
            _context.DrawIndexed((uint) _numberOfIndices, 0,0);
            
            //_context.Finialize(_device.ImmediateContext);
            _numberOfIndices = 0;
            _numberOfVertices = 0;
        }

        public void Dispose()
        {
            _perObjectConstantBuffer.Dispose();
            _perFrameConstantBuffer.Dispose();
            _inputLayout.Dispose();
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _indexBuffer.Dispose();
            _vertexBuffer.Dispose();
        }
    }
}
