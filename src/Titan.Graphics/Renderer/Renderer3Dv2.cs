using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Camera;
using Titan.Graphics.Layout;
using Titan.Graphics.Models;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;
using Titan.Windows.Input;

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
        private readonly IInputManager _inputManager;
        private readonly IConstantBuffer<PerFrameContantBuffer> _perFrameConstantBuffer;
        private readonly IConstantBuffer<LightsConstantBuffer> _lightsConstantBuffer;
        private readonly IConstantBuffer<PerObjectContantBuffer> _perObjectConstantBuffer;
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;
        private ICamera _camera;

        private readonly ISampler _sampler;

        private readonly List<(Vector3 position, Color color)> _lights = new List<(Vector3 position, Color color)>();

        public Renderer3Dv2(IDevice device, IBlobReader blobReader, ICameraFactory cameraFactory, IInputManager inputManager)
        {
            _device = device;
            _inputManager = inputManager;

            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);
     
           _inputLayout = device.CreateInputLayout(new VertexLayout(4).Append("Position", VertexLayoutTypes.Position3D).Append("Normal", VertexLayoutTypes.Position3D).Append("Texture", VertexLayoutTypes.Texture2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
           _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>();
           _lightsConstantBuffer = device.CreateConstantBuffer<LightsConstantBuffer>();
           _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>();

            _camera = cameraFactory.CreatePerspectiveCamera();

            _sampler = device.CreateSampler();
        }

        //private float _rotationX = 0f;
        //private float _rotationZ = 0f;
        private float _rotationY = 0f;

        public void Begin()
        {
            _perFrameConstantBuffer.Update(new PerFrameContantBuffer {ViewProjection = _camera.ViewProjection, View = _camera.View});
            _perFrameConstantBuffer.BindToVertexShader(PerFrameSlot);

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
        }


        private IVertexBuffer _lastVertexBuffer = null;
        private ITexture2D _lastTexture = null;
        public void Render(IMesh mesh, in Matrix4x4 worldMatrix, ITexture2D texture)
        {
            #region TestCode

            if (_inputManager.Keyboard.IsKeyDown(KeyCode.W))
            {
                _camera.Move(new Vector3(0, 0, -0.02f));
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.S))
            {
                _camera.Move(new Vector3(0, 0, 0.02f));
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.A))
            {
                _camera.Move(new Vector3(0.02f, 0,0 ));
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.D))
            {
                _camera.Move(new Vector3(-0.02f, 0, 0));
            }

            if (_inputManager.Keyboard.IsKeyDown(KeyCode.E))
            {
                _camera.RotateY(_rotationY -= 0.02f);
            }
            if (_inputManager.Keyboard.IsKeyDown(KeyCode.Q))
            {
                _camera.RotateY(_rotationY += 0.02f);
            }
            #endregion

            _perObjectConstantBuffer.Update(new PerObjectContantBuffer {World = worldMatrix});
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
    }

}
