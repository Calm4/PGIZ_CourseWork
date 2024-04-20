using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab01;
using SharpDX;
using SharpDX.Direct3D11;
using static Lab02.Renderer;
using Buffer11 = SharpDX.Direct3D11.Buffer;

namespace Lab02
{
    class MeshObject : Game3DObject, IDisposable
    {
        private DirectX3DGraphics _directX3DGraphics;

        private int _verticesCount;
        private Renderer.VertexDataStruct[] _vertices;
        private Buffer11 _vertexBufferObject;
        private VertexBufferBinding _vertexBufferBinding;
        public VertexBufferBinding VertexBufferBinding { get => _vertexBufferBinding; }

        private int _indicesCount;
        public int IndicesCount { get => _indicesCount; }
        private uint[] _indices;
        private Buffer11 _indicesBufferObject;
        public Buffer11 IndicesBufferObject { get => _indicesBufferObject; }

        public Buffer11 _materialBufferObject;
        public Buffer11 MaterialBufferObject { get => _materialBufferObject; }

        public MeshObject(DirectX3DGraphics directX3DGraphics, Vector4 position, float yaw, float pitch, float roll, Renderer.VertexDataStruct[] vertices, uint[] indices) 
            : base(position, yaw, pitch, roll)
        {
            _directX3DGraphics = directX3DGraphics;
            _vertices = vertices;
            _verticesCount = _vertices.Length;
            _indices = indices;
            _indicesCount = _indices.Length;

            _vertexBufferObject = Buffer11.Create(_directX3DGraphics.Device, BindFlags.VertexBuffer, _vertices, Utilities.SizeOf<Renderer.VertexDataStruct>() * _verticesCount);
            _vertexBufferBinding = new VertexBufferBinding(_vertexBufferObject, Utilities.SizeOf<Renderer.VertexDataStruct>(), 0);
            _indicesBufferObject = Buffer11.Create(_directX3DGraphics.Device, BindFlags.IndexBuffer, _indices, Utilities.SizeOf<uint>() * _indicesCount);

            /*_materialBufferObject = new Buffer11(_directX3DGraphics.Device,
                Utilities.SizeOf<MaterialProperties>(),
                ResourceUsage.Default, // was dynamic
                BindFlags.ConstantBuffer,
                CpuAccessFlags.Write, // 0 ?
                ResourceOptionFlags.None,
                0);*/
        }
        public Vector3 GetForwardVector()
        {
            Matrix rotation = Matrix.RotationYawPitchRoll(_yaw, _pitch, _roll);
            return Vector3.TransformNormal(Vector3.UnitZ, rotation);
        }

        public Vector3 GetRightVector()
        {
            Matrix rotation = Matrix.RotationYawPitchRoll(_yaw, _pitch, _roll);
            return Vector3.TransformNormal(Vector3.UnitX, rotation);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _indicesBufferObject);
            Utilities.Dispose(ref _vertexBufferObject);
        }
    }
}
