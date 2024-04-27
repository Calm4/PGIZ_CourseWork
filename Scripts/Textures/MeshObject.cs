using System;
using Lab01.App.Scripts.DirectX;
using Lab01.App.Scripts.Game;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer11 = SharpDX.Direct3D11.Buffer;

namespace Lab01.App.Scripts.Textures
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
