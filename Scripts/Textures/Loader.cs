using System;
using System.Collections.Generic;
using Lab01.App.Scripts.DirectX;
using ObjLoader.Loader.Loaders;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.WIC;

namespace Lab01.App.Scripts.Textures
{
    public class Loader : IDisposable
    {
        private DirectX3DGraphics _directX3DGraphics;
        private ImagingFactory _imagingFactory;

        public Loader(DirectX3DGraphics directX3DGraphics)
        {
            _directX3DGraphics = directX3DGraphics;
            _imagingFactory = new ImagingFactory();
        }

        public Texture LoadTextureFromFile(string fileName,
            SamplerState samplerState)
        {
            BitmapDecoder decoder = new BitmapDecoder(_imagingFactory,
                fileName, DecodeOptions.CacheOnDemand);
            BitmapFrameDecode bitMapFirstFrame = decoder.GetFrame(0);
            Utilities.Dispose(ref decoder);

            FormatConverter imageFormatConverter = new FormatConverter(_imagingFactory);
            imageFormatConverter.Initialize(bitMapFirstFrame,
                PixelFormat.Format32bppRGBA, BitmapDitherType.None, null, 0.0,
                BitmapPaletteType.Custom);
            int stride = imageFormatConverter.Size.Width * 4;
            DataStream buffer = new DataStream(
                imageFormatConverter.Size.Height * stride, true, true);
            imageFormatConverter.CopyPixels(stride, buffer);

            int width = imageFormatConverter.Size.Width;
            int height = imageFormatConverter.Size.Height;

            Texture2DDescription textureDescription = new Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.R8G8B8A8_UNorm,
                SampleDescription = _directX3DGraphics.SampleDescription,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            Texture2D textureObject = new Texture2D(_directX3DGraphics.Device,
                textureDescription, new DataRectangle(buffer.DataPointer,
                stride));
            ShaderResourceViewDescription shaderResourceViewDescription =
                new ShaderResourceViewDescription()
                {
                    Dimension = ShaderResourceViewDimension.Texture2D,
                    Format = Format.R8G8B8A8_UNorm,
                    Texture2D =
                        new ShaderResourceViewDescription.Texture2DResource
                        {
                            MostDetailedMip = 0,
                            MipLevels = -1
                        }
                };
            ShaderResourceView shaderResourceView =
                new ShaderResourceView(_directX3DGraphics.Device, textureObject,
                shaderResourceViewDescription);

            Utilities.Dispose(ref imageFormatConverter);

            return new Texture(textureObject, shaderResourceView, width, height,
                samplerState);

        }

        public MeshObject LoadMeshObjectFromObjFile(LoadResult loadResult, Vector4 position, float yaw, float pitch, float roll, ref Texture texture, SamplerState sampler, float sizeMultiplier = 1f)
        {
            var currentGroup = loadResult.Groups[0];

            List<uint> indices = new List<uint>();

            List<Renderer.VertexDataStruct> vertices = new List<Renderer.VertexDataStruct>();

            foreach (var face in currentGroup.Faces)
            {
                for (int i = face.Count - 1; i >= 0; i--)
                {
                    var vertexPosition = loadResult.Vertices[face[i].VertexIndex - 1];
                    ObjLoader.Loader.Data.VertexData.Texture texturePosition;
                    if (loadResult.Textures.Count == 0)
                    {
                        Random random = new Random();
                        texturePosition =
                            new ObjLoader.Loader.Data.VertexData.Texture(random.NextFloat(0f, 1f),
                                random.NextFloat(0f, 1f));
                    }
                    else
                    {
                        texturePosition = loadResult.Textures[face[i].TextureIndex - 1];
                    }

                    var normalPosition = loadResult.Normals[face[i].NormalIndex - 1];
                    vertices.Add(new Renderer.VertexDataStruct
                    {
                        Position = new Vector4(vertexPosition.X * sizeMultiplier, vertexPosition.Y * sizeMultiplier, vertexPosition.Z * sizeMultiplier, 1.0f),
                        Tex0 = new Vector2(texturePosition.X, 1.0f - texturePosition.Y),
                        Normal = new Vector4(normalPosition.X, normalPosition.Y, normalPosition.Z, 1.0f)
                    });
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                indices.Add((uint)i);
            }

            if (loadResult.Textures.Count != 0)
                texture = LoadTextureFromFile(currentGroup.Material.DiffuseTextureMap, sampler);

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices.ToArray(), indices.ToArray());
        }

        public MeshObject MakePlot(Vector4 position, float yaw, float pitch, float roll, float height, float weight, float yValue)
        {
            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[5]
            {
                new Renderer.VertexDataStruct
                {
                    Position = new Vector4(-weight, yValue, height, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f)
                },
                new Renderer.VertexDataStruct
                {
                    Position = new Vector4(weight, yValue, height, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
                new Renderer.VertexDataStruct
                {
                    Position = new Vector4(weight, yValue, -height, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 1.0f),
                },
                new Renderer.VertexDataStruct
                {
                    Position = new Vector4 (weight, yValue, -height, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 1.0f),
                },
                new Renderer.VertexDataStruct
                {
                    Position = new Vector4 (-weight, yValue, -height, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 1.0f),
                }
            };

            uint[] indices = new uint[6]
            {
                2, 1, 0,
                0, 4, 3
            };

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);
        }


        public MeshObject MakePlot(Vector4 position, float yaw, float pitch, float roll, float height, float weight, float yValue, ref BoundingBox boundingBox)
        {
            MeshObject plot = MakePlot(position, yaw, pitch, roll, height, weight, yValue);


            boundingBox = new BoundingBox(new Vector3(-weight, yValue, -height) + new Vector3(position.X, position.Y, position.Z), new Vector3(weight, yValue, height) + new Vector3(position.X, position.Y, position.Z));

            return plot;
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _imagingFactory);
        }
    }
}
