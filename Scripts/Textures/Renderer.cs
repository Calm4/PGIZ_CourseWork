using System;
using System.Runtime.InteropServices;
using Lab01.App.Scripts.DirectX;
using Lab01.App.Scripts.Environment;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer11 = SharpDX.Direct3D11.Buffer;
using Device11 = SharpDX.Direct3D11.Device;

namespace Lab01.App.Scripts.Textures
{
    public class Renderer : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct VertexDataStruct
        {
            public Vector4 Position;
            public Vector4 Normal;
            public Vector2 Tex0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PerObjectConstantBuffer
        {
            public Matrix WorldMatrix;
            public Matrix InverseTransposeWorldMatrix;
            public Matrix WorldViewProjectionMatrix;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PerObjectPixelShaderConstantBuffer
        {
            public MaterialProperties MaterialProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LightConstantBuffer
        {
            public LightProperties LightProperties;
        }

        private DirectX3DGraphics _directX3DGraphics;
        private Device11 _device;
        private DeviceContext _deviceContext;

        private VertexShader _vertexShader;
        private PixelShader _pixelShader;
        private ShaderSignature _shaderSignature;
        private InputLayout _inputLayout;

        private PerObjectConstantBuffer _perObjectConstantBuffer;
        private Buffer11 _perObjectConstantBufferObject;

        private PerObjectPixelShaderConstantBuffer _perObjectPixelShaderConstantBuffer;
        private Buffer11 _perObjectPixelShaderConstantBufferObject;

        private LightConstantBuffer _lightConstantBuffer;

        private ConstantBuffer<LightConstantBuffer> _lightConstantBufferObject;

        private SamplerState _anisotropicSampler;
        public SamplerState AnisotropicSampler { get => _anisotropicSampler; }

        public Renderer(DirectX3DGraphics directX3DGraphics)
        {
            _directX3DGraphics = directX3DGraphics;
            _device = _directX3DGraphics.Device;
            _deviceContext = _directX3DGraphics.DeviceContext;

            CompilationResult vertexShaderByteCode = ShaderBytecode.CompileFromFile("vertex.hlsl", "vertexShader", "vs_5_0"); 
            _vertexShader = new VertexShader(_device, vertexShaderByteCode);

            CompilationResult pixelShaderByteCode = ShaderBytecode.CompileFromFile("pixel.hlsl", "pixelShader", "ps_5_0");
            _pixelShader = new PixelShader(_device, pixelShaderByteCode);

            InputElement[] inputElements = new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0, InputClassification.PerVertexData, 0),
                new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, 16, 0, InputClassification.PerVertexData, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 32, 0, InputClassification.PerVertexData, 0)
            };

            _shaderSignature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            _inputLayout = new InputLayout(_device, _shaderSignature, inputElements);

            Utilities.Dispose(ref vertexShaderByteCode);
            Utilities.Dispose(ref pixelShaderByteCode);

            _deviceContext.InputAssembler.InputLayout = _inputLayout;
            _deviceContext.VertexShader.Set(_vertexShader);
            _deviceContext.PixelShader.Set(_pixelShader);

            SamplerStateDescription samplerStateDescription =
                new SamplerStateDescription
                {
                    Filter = Filter.Anisotropic,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Clamp,
                    MipLodBias = 0.0f,
                    MaximumAnisotropy = 16,
                    ComparisonFunction = Comparison.Never,
                    BorderColor = new SharpDX.Mathematics.Interop.RawColor4(
                        1.0f, 1.0f, 1.0f, 1.0f),
                    MinimumLod = 0,
                    MaximumLod = float.MaxValue
                };
            _anisotropicSampler = new SamplerState(_directX3DGraphics.Device,
                samplerStateDescription);
        }

        public void CreateConstantBuffers()
        {
            _perObjectConstantBufferObject = new Buffer11(
                _device,
                Utilities.SizeOf<PerObjectConstantBuffer>(),
                ResourceUsage.Dynamic, // was dynamic
                BindFlags.ConstantBuffer,
                CpuAccessFlags.Write, // 0 ?
                ResourceOptionFlags.None,
                0);

            _perObjectPixelShaderConstantBufferObject = new Buffer11(
                _device,
                Utilities.SizeOf<PerObjectPixelShaderConstantBuffer>(),
                ResourceUsage.Dynamic,
                BindFlags.ConstantBuffer,
                CpuAccessFlags.Write,
                ResourceOptionFlags.None,
                0);

            var size = Marshal.SizeOf<LightConstantBuffer>();

            var tstSize = Utilities.SizeOf<Vector4>() * 2 + Utilities.SizeOf<Light>() * 4;

            _lightConstantBufferObject = new ConstantBuffer<LightConstantBuffer>(_device);
        }



        public void CreateLightBuffer(LightProperties lightProperties)
        {
            //_lightConstantBufferObject = Buffer11.Create(_directX3DGraphics.Device, BindFlags.ConstantBuffer, );
        }

        public void SetLightConstantBuffer(LightProperties lightProperties)
        {
            _lightConstantBuffer.LightProperties = lightProperties;
        }


        public void SetPerObjectConstantBuffer(MaterialProperties materialProperties)
        {
            _perObjectPixelShaderConstantBuffer.MaterialProperties = materialProperties;
        }

        public void BeginRender()
        {
            _directX3DGraphics.ClearBuffers(Color.Black);
        }

        public void UpdatePerObjectConstantBuffers(Matrix world, Matrix view, Matrix projection)
        {

            _perObjectConstantBuffer.WorldMatrix = world;

            _perObjectConstantBuffer.InverseTransposeWorldMatrix = Matrix.Transpose(Matrix.Invert(world));

            Matrix viewProjectionMatrix = view * projection;

            _perObjectConstantBuffer.WorldViewProjectionMatrix = world * viewProjectionMatrix; 
          
            _deviceContext.MapSubresource(_perObjectConstantBufferObject, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out DataStream dataStream);
            dataStream.Write(_perObjectConstantBuffer);
            _deviceContext.UnmapSubresource(_perObjectConstantBufferObject, 0);
            _deviceContext.VertexShader.SetConstantBuffer(0, _perObjectConstantBufferObject);

            _deviceContext.MapSubresource(_perObjectPixelShaderConstantBufferObject, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out DataStream pixelDataStream);
            pixelDataStream.Write(_perObjectPixelShaderConstantBuffer);
            _deviceContext.UnmapSubresource(_perObjectPixelShaderConstantBufferObject, 0);
            _deviceContext.PixelShader.SetConstantBuffer(0, _perObjectPixelShaderConstantBufferObject);



            _lightConstantBufferObject.UpdateValue(_lightConstantBuffer);


            _deviceContext.PixelShader.SetConstantBuffer(1, _lightConstantBufferObject.Buffer);


            
        }

        public void EndRender()
        {
            _directX3DGraphics.SwapChain.Present(1, PresentFlags.Restart);
        }

        public void SetTexture(Texture texture)
        {
            _deviceContext.PixelShader.SetShaderResource(0,
                texture.ShaderResourceView);
            _deviceContext.PixelShader.SetSampler(0,
                texture.SamplerState);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _anisotropicSampler);
            Utilities.Dispose(ref _perObjectConstantBufferObject);
            Utilities.Dispose(ref _perObjectPixelShaderConstantBufferObject);
            Utilities.Dispose(ref _lightConstantBufferObject);
            Utilities.Dispose(ref _inputLayout);
            Utilities.Dispose(ref _shaderSignature);
            Utilities.Dispose(ref _pixelShader);
            Utilities.Dispose(ref _vertexShader);
        }

        public void RenderMeshObject(MeshObject meshObject)
        {
            _deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            _deviceContext.InputAssembler.SetVertexBuffers(0, meshObject.VertexBufferBinding);
            _deviceContext.InputAssembler.SetIndexBuffer(meshObject.IndicesBufferObject, Format.R32_UInt, 0);
            _deviceContext.DrawIndexed(meshObject.IndicesCount, 0, 0);
        }
    }

    public class ConstantBuffer<T> : IDisposable
    where T : struct
    {
        private readonly Device11 _device;
        private readonly Buffer11 _buffer;
        private readonly DataStream _dataStream;

        public Buffer11 Buffer
        {
            get { return _buffer; }
        }

        public ConstantBuffer(Device11 device)
        {
            _device = device;

            // If no specific marshalling is needed, can use
            // SharpDX.Utilities.SizeOf<T>() for better performance.
            int size = Marshal.SizeOf(typeof(T));

            _buffer = new Buffer11(device, new BufferDescription
            {
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.ConstantBuffer,
                SizeInBytes = size,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            });

            _dataStream = new DataStream(size, true, true);
        }

        public void UpdateValue(T value)
        {
            // If no specific marshalling is needed, can use 
            // dataStream.Write(value) for better performance.
            Marshal.StructureToPtr(value, _dataStream.DataPointer, false);

            var dataBox = new DataBox(_dataStream.DataPointer, 0, 0);
            _device.ImmediateContext.UpdateSubresource(dataBox, _buffer, 0);
        }

        public void Dispose()
        {
            if (_dataStream != null)
                _dataStream.Dispose();
            if (_buffer != null)
                _buffer.Dispose();
        }
    }
}
