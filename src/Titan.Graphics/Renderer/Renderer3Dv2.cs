using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Models;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer
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
                LightColors[parameter] = color.Red;
                LightColors[parameter + 1] = color.Green;
                LightColors[parameter + 2] = color.Blue;
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

        public Renderer3Dv2(IDevice device, IBlobReader blobReader)
        {
            _device = device;

            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(4).Append("Position", VertexLayoutTypes.Position3D).Append("Normal", VertexLayoutTypes.Position3D).Append("Texture", VertexLayoutTypes.Texture2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>();
            _lightsConstantBuffer = device.CreateConstantBuffer<LightsConstantBuffer>();
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>();
            _sampler = device.CreateSampler();
        }

        public void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            _perFrameConstantBuffer.Update(new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            _perFrameConstantBuffer.BindToVertexShader(PerFrameSlot);
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
            _lightsConstantBuffer.Update(lights);
            _lightsConstantBuffer.BindToPixelShader(0);

            _sampler.Bind();
            _inputLayout.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();
            //_device.BeginRender();
        }


        private IVertexBuffer _lastVertexBuffer = null;
        private ITexture2D _lastTexture = null;
        public void Render(IMesh mesh, in Matrix4x4 worldMatrix, ITexture2D texture)
        {
            
            _perObjectConstantBuffer.Update(new PerObjectContantBuffer {World = Matrix4x4.Transpose(worldMatrix)});
            _perObjectConstantBuffer.BindToVertexShader(PerObjectSlot);
            
            texture.Bind();
            //mesh.IndexBuffer.Bind();

            if (mesh.VertexBuffer != _lastVertexBuffer)
            {
                mesh.VertexBuffer.Bind();
                _lastVertexBuffer = mesh.VertexBuffer;
            }

            if (_lastTexture != texture)
            {
                texture.Bind();
                _lastTexture = texture;
            }

            //_perObjectConstantBuffer.BindToPixelShader();

            _device.Draw(mesh.VertexBuffer.NumberOfVertices, 0);
            //_device.DrawIndexed(mesh.IndexBuffer.NumberOfIndices, 0, 0);

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
            _perObjectConstantBuffer.Dispose();
            _perFrameConstantBuffer.Dispose();
            _lightsConstantBuffer.Dispose();
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _inputLayout.Dispose();

        }

        public void SubmitLight(in Color color, in Vector3 position)
        {
            _lights.Add((position, color));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPrimitiveTopology(PrimitiveTopology topology) => _device.SetPrimitiveTopology(topology);
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


        private readonly LineVertex[] _vertices = new LineVertex[60000];
        private readonly short[] _indices = new short[10000];
        private int _numberOfVertices;
        private int _numberOfIndices;

        public RendererDebug3Dv3(IDevice device, IBlobReader blobReader)
        {
            _device = device;
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/LineVertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/LinePixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("Position", VertexLayoutTypes.Position3D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>();
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>();

            _indexBuffer = _device.CreateIndexBuffer(10000);
            _vertexBuffer = _device.CreateVertexBuffer<LineVertex>(60000);
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
            _indices[_numberOfIndices++] = (short) (offset + 1);
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



            //_indices[_numberOfIndices++] = (ushort)(offset + 2);
            //_indices[_numberOfIndices++] = (ushort)(offset + 5);
            //_indices[_numberOfIndices++] = offset;
            //_indices[_numberOfIndices++] = (ushort)(offset + 6);
            //_indices[_numberOfIndices++] = offset;
            //_indices[_numberOfIndices++] = (ushort)(offset + 7);

        }

        public void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            _perFrameConstantBuffer.Update(new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            _perFrameConstantBuffer.BindToVertexShader(0);
        }

        public void Render()
        {
            _device.SetPrimitiveTopology(PrimitiveTopology.LineList);

            _vertexBuffer.SetData(_vertices, _numberOfVertices);
            _indexBuffer.SetData(_indices, _numberOfIndices);

            _inputLayout.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();
            _vertexBuffer.Bind();
            _indexBuffer.Bind();

            _device.DrawIndexed((uint) _numberOfIndices, 0, 0);
            
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
