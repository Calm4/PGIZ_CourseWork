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
    class Loader : IDisposable
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

        public MeshObject MakeIcosahedron(Vector4 position, float yaw, float pitch, float roll)
        {
            float firstFormula = (float)(1.0 / Math.Sqrt(5));
            float secondFormula = (float)((1.0 - firstFormula) / 2.0);
            float thirdFormula = (float)((1.0 + firstFormula) / 2.0);
            float fourthFormula = (float)((-1.0 - firstFormula) / 2.0);

            Vector4[] icosahedronVertices =
            {
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(firstFormula, 2*firstFormula, 0.0f, 1.0f),
                new Vector4(firstFormula, secondFormula, (float)Math.Sqrt(thirdFormula), 1.0f),
                new Vector4(firstFormula, fourthFormula, (float)Math.Sqrt(secondFormula), 1.0f),
                new Vector4(firstFormula, fourthFormula, (float)(-Math.Sqrt(secondFormula)), 1.0f),
                new Vector4(firstFormula, secondFormula, (float)(-Math.Sqrt(thirdFormula)), 1.0f),
                new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(-firstFormula, (float)(-2.0*firstFormula), 0.0f, 1.0f),
                new Vector4(-firstFormula, -secondFormula, (float)(-Math.Sqrt(thirdFormula)), 1.0f),
                new Vector4(-firstFormula, thirdFormula, (float)(-Math.Sqrt(secondFormula)), 1.0f),
                new Vector4(-firstFormula, thirdFormula, (float)Math.Sqrt(secondFormula), 1.0f),
                new Vector4(-firstFormula, -secondFormula, (float)Math.Sqrt(thirdFormula), 1.0f),
            };

            uint[] firstIndices = new uint[]
            {
                2, 1, 0,
                3, 2, 0,
                4, 3, 0,
                5, 4, 0,
                1, 5, 0,

                10, 9, 1,
                1, 9, 5,
                8, 5, 9,
                5, 8, 4,
                4, 8, 7,

                4, 7, 3,
                3, 7, 11,
                3, 11, 2,
                2, 11, 10,
                2, 10, 1,

                6, 7, 8,
                6, 8, 9,
                6, 9, 10,
                6, 10, 11,
                6, 11, 7
            };

            Vector2[] icosahedronTexCoords =
            {
                // верхняя левая текстура
                new Vector2(0f, 0f), new Vector2(0.5f,0f), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0f),
                // верхняя правая текстура
                new Vector2(0.5f, 0f), new Vector2(1f,0f), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(1.0f, 0.5f), new Vector2(1f, 0f),
                // левая нижняя текстура
                new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0,1f),
                new Vector2(0, 1f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 1f),
                // правая нижняя текстура
                new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f,1f),
                new Vector2(1f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f),

                // верхняя левая текстура
                new Vector2(0, 0), new Vector2(0.5f,0), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(0, 0.5f), new Vector2(0, 0),
                // верхняя правая текстура
                new Vector2(0.5f, 0), new Vector2(1f,0), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0),
                // левая нижняя текстура
                new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0,1f),
                new Vector2(0, 1f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 1f),
                // правая нижняя текстура
                new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f,1f),
                new Vector2(1f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f),

                // верхняя левая текстура
                new Vector2(0, 0), new Vector2(0.5f,0), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(0, 0.5f), new Vector2(0, 0),
                // верхняя правая текстура
                new Vector2(0.5f, 0), new Vector2(1f,0), new Vector2(0.5f,0.5f),
                new Vector2(0.5f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0),

            };

            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[firstIndices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Renderer.VertexDataStruct
                {
                    Position = icosahedronVertices[firstIndices[i]],
                    Tex0 = icosahedronTexCoords[i]
                };
            }

            uint[] indices = new uint[vertices.Length];

            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = (uint)i;
            }

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);
        }

       /* public MeshObject LoadMeshObjectFromObjFile(LoadResult loadResult, Vector4 position, float yaw, float pitch, float roll, ref Texture texture, SamplerState sampler)
        {
            var currentGroup = loadResult.Groups[0];

            List<uint> indices = new List<uint>();

            List<Renderer.VertexDataStruct> vertices = new List<Renderer.VertexDataStruct>();

            foreach(var face in currentGroup.Faces)
            {
                for (int i = face.Count - 1; i >= 0; i--)
                {
                    var vertexPosition = loadResult.Vertices[face[i].VertexIndex - 1];
                    var texturePosition = loadResult.Textures[face[i].TextureIndex - 1];
                    var normalPosition = loadResult.Normals[face[i].NormalIndex - 1];
                    vertices.Add(new Renderer.VertexDataStruct
                    {
                        Position = new Vector4(vertexPosition.X, vertexPosition.Y, vertexPosition.Z, 1.0f),
                        Tex0 = new Vector2(texturePosition.X, 1.0f - texturePosition.Y),
                        Normal = new Vector4(normalPosition.X, normalPosition.Y, normalPosition.Z, 1.0f)
                    });
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                indices.Add((uint)i);
            }

            texture = LoadTextureFromFile(currentGroup.Material.DiffuseTextureMap, sampler);

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices.ToArray(), indices.ToArray());
        }*/
        
         public MeshObject LoadMeshObjectFromObjFile(LoadResult loadResult, Vector4 position, float yaw, float pitch, float roll, ref Texture texture, SamplerState sampler, float sizeMultiplier = 1f)
                {
                    var currentGroup = loadResult.Groups[0];

                    List<uint> indices = new List<uint>();

                    List<Renderer.VertexDataStruct> vertices = new List<Renderer.VertexDataStruct>();

                    foreach(var face in currentGroup.Faces)
                    {
                        for (int i = face.Count - 1; i >= 0; i--)
                        {
                            var vertexPosition = loadResult.Vertices[face[i].VertexIndex - 1] ;
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
                            //
                            vertices.Add(new Renderer.VertexDataStruct
                            {
                                Position = new Vector4(vertexPosition.X * sizeMultiplier, vertexPosition.Y * sizeMultiplier, vertexPosition.Z * sizeMultiplier, 1.0f),
                                Tex0 = new Vector2(texturePosition.X, 1.0f - texturePosition.Y),
                                //Tex0 = new Vector2(random.NextFloat(0f, 1f), random.NextFloat(0f, 1f)),
                                Normal = new Vector4(normalPosition.X, normalPosition.Y, normalPosition.Z, 1.0f)
                            });
                        }
                    }

                    for (int i = 0; i < vertices.Count; i++)
                    {
                        indices.Add((uint)i);
                    }

                    if(loadResult.Textures.Count != 0)
                        texture = LoadTextureFromFile(currentGroup.Material.DiffuseTextureMap, sampler);

                    return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices.ToArray(), indices.ToArray());
                }
        
        public MeshObject MakeIcosahedron(Vector4 position, float yaw, float pitch, float roll, ref BoundingSphere boundingSphere)
        {
            MeshObject icosahedron = MakeIcosahedron(position, yaw, pitch, roll);

            boundingSphere = new BoundingSphere(new Vector3(position.X, position.Y, position.Z), 1.0f);

            return icosahedron;
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

        public MeshObject MakeLittleTetrahedron(Vector4 position, float yaw, float pitch, float roll)
        {
            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[12]
            {
                new Renderer.VertexDataStruct // 0
                {
                    Position = new Vector4(0.0f, 0.1f, 0.03f, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 1
                {
                    Position = new Vector4 (0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 2
                {
                    Position = new Vector4 (-0.1f, -0.1f, 0.1f, 1f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 3
                {
                    Position = new Vector4 (0.0f, 0.1f, 0.03f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
                new Renderer.VertexDataStruct // 4
                {
                    Position = new Vector4 (0.0f, -0.1f, -0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f)
                },
                new Renderer.VertexDataStruct // 5
                {
                    Position = new Vector4 (-0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f)
                },
                new Renderer.VertexDataStruct // 6
                {
                    Position = new Vector4 (0.0f, 0.1f, 0.03f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 7
                {
                    Position = new Vector4 (0.0f, -0.1f, -0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 8
                {
                    Position = new Vector4 (0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
                new Renderer.VertexDataStruct // 9
                {
                    Position = new Vector4 (0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 10
                {
                    Position = new Vector4 (0.0f, -0.1f, -0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 11
                {
                    Position = new Vector4 (-0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
            };

            uint[] indices = new uint[12]
            {
               0, 1, 2,
               3, 5, 4,
               6, 7, 8,
               9, 10, 11

            };

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);
        }

        public MeshObject MakeCube(Vector4 position, float yaw, float pitch, float roll)
        {
            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[8]
            {
                new Renderer.VertexDataStruct // 0
                {
                    Position = new Vector4(0.0f, 0.0f, 0.0f, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 1
                {
                    Position = new Vector4 (0.1f, 0.0f, 0.0f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 2
                {
                    Position = new Vector4 (0.1f, -0.1f, 0.0f, 1f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 3
                {
                    Position = new Vector4(0.0f, -0.1f, 0.0f, 1.0f),
                    Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 4
                {
                    Position = new Vector4 (0.0f, 0.0f, 0.1f, 1f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 5
                {
                    Position = new Vector4 (0.1f, 0.0f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f)
                },
                new Renderer.VertexDataStruct // 6
                {
                    Position = new Vector4 (0.1f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 7
                {
                    Position = new Vector4 (0.0f, -0.1f, 0.1f, 1.0f),
                    Normal = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
            };

            uint[] indices = new uint[36]
            {
               2, 1, 0,
               3, 2, 0,
               3, 0, 4,
               7, 3, 4,
               5, 4, 7,
               5, 7, 6,
               2, 5, 1,
               6, 2, 5,
               7, 6, 2,
               3, 2, 7,
               1, 5, 4,
               0, 1, 4
            };

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);
        }

        public MeshObject MakeCube(Vector4 position, float yaw, float pitch, float roll, ref BoundingBox collider)
        {
            MeshObject cube = MakeCube(position, yaw, pitch, roll);

            collider = new BoundingBox(new Vector3(position.X, position.Y, position.Z) + new Vector3(0.0f, -0.1f, 0.0f), new Vector3(position.X, position.Y, position.Z) + new Vector3(0.1f, 0.0f, 0.1f));

            return cube;
        }

        public MeshObject MakeSphere(Vector4 centerPosition, float radius, float yaw, float pitch, float roll, int tessellation = 16)
        {
            centerPosition.W = 1.0f;


            int verticalSegments = tessellation;
            int horizontalSegments = tessellation * 2;

            var vertices = new Renderer.VertexDataStruct[(verticalSegments + 1) * (horizontalSegments + 1)];
            var indices = new uint[(verticalSegments) * (horizontalSegments + 1) * 6];


            int vertexCount = 0;
            // Create rings of vertices at progressively higher latitudes.
            for (int i = 0; i <= verticalSegments; i++)
            {
                float v = 1.0f - (float)i / verticalSegments;

                var latitude = (float)((i * Math.PI / verticalSegments) - Math.PI / 2.0);
                var dy = (float)Math.Sin(latitude) * 1.15f;
                var dxz = (float)Math.Cos(latitude) * 1.05f;

                // Create a single ring of vertices at this latitude.
                for (int j = 0; j <= horizontalSegments; j++)
                {
                    float u = (float)j / horizontalSegments;

                    var longitude = (float)(j * 2.0 * Math.PI / horizontalSegments);
                    var dx = (float)Math.Sin(longitude);
                    var dz = (float)Math.Cos(longitude);

                    dx *= dxz;
                    dz *= dxz;

                    var normal = new Vector4(dx, dy, dz, 1.0f);
                    var textureCoordinate = new Vector2(u, v);

                    vertices[vertexCount++] = new Renderer.VertexDataStruct
                    {
                        Position = normal*radius,
                        Normal = normal, 
                        Tex0 = textureCoordinate
                    };
                }
            }

            // Fill the index buffer with triangles joining each pair of latitude rings.
            uint stride = (uint)horizontalSegments + 1;

            uint indexCount = 0;
            for (uint i = 0; i < verticalSegments; i++)
            {
                for (uint j = 0; j <= horizontalSegments; j++)
                {
                    uint nextI = i + 1;
                    uint nextJ = (j + 1) % stride;

                    indices[indexCount++] = (i * stride + j);
                    indices[indexCount++] = (nextI * stride + j);
                    indices[indexCount++] = (i * stride + nextJ);

                    indices[indexCount++] = (i * stride + nextJ);
                    indices[indexCount++] = (nextI * stride + j);
                    indices[indexCount++] = (nextI * stride + nextJ);
                }
            }

            return new MeshObject(_directX3DGraphics, centerPosition, yaw, pitch, roll, vertices, indices);

        }

        public MeshObject MakeSphereCollider(BoundingSphere boundingSphere, float yaw, float pitch, float roll)
        {
            return MakeSphere((Vector4)boundingSphere.Center, boundingSphere.Radius, yaw, pitch, roll);
        }

        public MeshObject MakeBoxCollider(BoundingBox boundingBox, Vector4 position, float yaw, float pitch, float roll)
        {

            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[8]
            {
                new Renderer.VertexDataStruct // 0
                {
                    Position = new Vector4(boundingBox.Minimum.X, boundingBox.Maximum.Y, boundingBox.Minimum.Z, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 1
                {
                    Position = new Vector4 (boundingBox.Maximum.X, boundingBox.Maximum.Y, boundingBox.Minimum.Z, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 2
                {
                    Position = new Vector4 (boundingBox.Maximum.X, boundingBox.Minimum.Y, boundingBox.Minimum.Z, 1f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 3
                {
                    Position = new Vector4(boundingBox.Minimum.X, boundingBox.Minimum.Y, boundingBox.Minimum.Z, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 4
                {
                    Position = new Vector4 (boundingBox.Minimum.X, boundingBox.Maximum.Y, boundingBox.Maximum.Z, 1f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 5
                {
                    Position = new Vector4 (boundingBox.Maximum.X, boundingBox.Maximum.Y, boundingBox.Maximum.Z, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f)
                },
                new Renderer.VertexDataStruct // 6
                {
                    Position = new Vector4 (boundingBox.Maximum.X, boundingBox.Minimum.Y, boundingBox.Maximum.Z, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 7
                {
                    Position = new Vector4 (boundingBox.Minimum.X,boundingBox.Minimum.Y, boundingBox.Maximum.Z, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
            };

            uint[] indices = new uint[36]
            {
               2, 1, 0,
               3, 2, 0,
               3, 0, 4,
               7, 3, 4,
               5, 4, 7,
               5, 7, 6,
               2, 5, 1,
               6, 2, 5,
               7, 6, 2,
               3, 2, 7,
               1, 5, 4,
               0, 1, 4
            };

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);

        }

        public MeshObject MakeGrid(Vector4 position, float yaw, float pitch, float roll)
        {
            int frequency = 20;
            int width = 30;

            int index = 0;

            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[frequency * frequency * (width + 1) * (width + 1)];

            for (int i = -width / 2; i < width / 2 + 1; i++)
            {
                for (int di = 0; di < frequency; di++)
                {
                    float x = (float)i + (float)di / (float)frequency;
                    for (int j = -width / 2; j < width / 2 + 1; j++)
                    {
                        for (int dj = 0; dj < frequency; dj++)
                        {
                            float z = (float)j + (float)dj / (float)frequency;

                            vertices[index++] = new Renderer.VertexDataStruct
                            {
                                Position = new Vector4(x, -10.0f, z, 1.0f),
                                Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f)
                            };
                        }
                    }
                }

            }

            uint[] indices = new uint[frequency * frequency * width * width * 6];

            index = 0;

            for (int i = 0; i < (width - 1) * frequency; i++)
            {
                for (int j = 0; j < (width - 1) * frequency; j++)
                {
                    indices[index++] = (uint)(i * (width + 1) * frequency + j);
                    indices[index++] = (uint)(i * (width + 1) * frequency + j + 1);
                    indices[index++] = (uint)((i + 1) * (width + 1) * frequency + j + 1);

                    indices[index++] = (uint)((i + 1) * (width + 1) * frequency + j + 1);
                    indices[index++] = (uint)((i + 1) * (width + 1) * frequency + j);
                    indices[index++] = (uint)(i * (width + 1) * frequency + j);
                }
            }

            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);

        }

        public MeshObject MakeTetrahedron(Vector4 position, float yaw, float pitch, float roll)
        {

            Vector4 firstNormal = new Vector4();
            Renderer.VertexDataStruct[] vertices = new Renderer.VertexDataStruct[12]
            {
                new Renderer.VertexDataStruct // 0
                {
                    Position = new Vector4(0.0f, 1.0f, 0.3f, 1.0f),
                    Normal = new Vector4(0.0f, 0.33f, 0.9438f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 1
                {
                    Position = new Vector4 (1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(0.0f, 0.33f, 0.9438f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 2
                {
                    Position = new Vector4 (-1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(0.0f, 0.33f, 0.9438f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 3
                {
                    Position = new Vector4 (0.0f, 1.0f, 0.3f, 1.0f),
                    Normal = new Vector4(-0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
                new Renderer.VertexDataStruct // 4
                {
                    Position = new Vector4 (0.0f, -1.0f, -1.0f, 1.0f),
                    Normal = new Vector4(-0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f)
                },
                new Renderer.VertexDataStruct // 5
                {
                    Position = new Vector4 (-1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(-0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(0.0f, 0.5f)
                },
                new Renderer.VertexDataStruct // 6
                {
                    Position = new Vector4 (0.0f, 1.0f, 0.3f, 1.0f),
                    Normal = new Vector4(0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.0f),
                },
                new Renderer.VertexDataStruct // 7
                {
                    Position = new Vector4 (0.0f, -1.0f, -1.0f, 1.0f),
                    Normal = new Vector4(0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 8
                {
                    Position = new Vector4 (1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(0.8588f, 0.279f, -0.429f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
                new Renderer.VertexDataStruct // 9
                {
                    Position = new Vector4 (1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.0f),
                },
                new Renderer.VertexDataStruct // 10
                {
                    Position = new Vector4 (0.0f, -1.0f, -1.0f, 1.0f),
                    Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(1.0f, 0.5f),
                },
                new Renderer.VertexDataStruct // 11
                {
                    Position = new Vector4 (-1.0f, -1.0f, 1.0f, 1.0f),
                    Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                    Tex0 = new Vector2(0.5f, 0.5f),
                },
            };

            /*Vector3 vectorA = (Vector3)vertices[1].Position - (Vector3)vertices[0].Position;
            Vector3 vectorB = (Vector3)vertices[2].Position - (Vector3)vertices[0].Position;

            Vector3 vectorC = (new Vector3(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y,
                vectorA.Z * vectorB.X - vectorA.X * vectorB.Z, 
                vectorA.X * vectorB.Y - vectorA.Y * vectorB.X)) / 4.0f;

            Vector3 vectorC = Vector3.Cross(vectorA, vectorB);

            vectorC.Normalize();

            Vector4 vector4C = new Vector4(-vectorC.X, -vectorC.Y, -vectorC.Z, 1.0f);

            vertices[0].Normal = vector4C;
            vertices[1].Normal = vector4C;
            vertices[2].Normal = vector4C;

            vectorA = (Vector3)vertices[4].Position - (Vector3)vertices[3].Position;
            vectorB = (Vector3)vertices[5].Position - (Vector3)vertices[3].Position;

            /*vectorC = (new Vector3(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y,
                vectorA.Z * vectorB.X - vectorA.X * vectorB.Z,
                vectorA.X * vectorB.Y - vectorA.Y * vectorB.X)) / 4.0f;

            vectorC = Vector3.Cross(vectorA, vectorB);

            vectorC.Normalize();

            vector4C = new Vector4(-vectorC.X, -vectorC.Y, -vectorC.Z, 1.0f);

            vertices[3].Normal = vector4C;
            vertices[4].Normal = vector4C;
            vertices[5].Normal = vector4C;


            vectorA = (Vector3)vertices[7].Position - (Vector3)vertices[6].Position;
            vectorB = (Vector3)vertices[8].Position - (Vector3)vertices[6].Position;

            //vectorC = (new Vector3(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y, vectorA.Z * vectorB.X - vectorA.X * vectorB.Z, vectorA.X * vectorB.Y - vectorA.Y * vectorB.X)) / 4.0f;

            vectorC = Vector3.Cross(vectorA, vectorB);

            vectorC.Normalize();

            vector4C = new Vector4(-vectorC.X, -vectorC.Y, -vectorC.Z, 1.0f);

            vertices[6].Normal = vector4C;
            vertices[7].Normal = vector4C;
            vertices[8].Normal = vector4C;


            vectorA = (Vector3)vertices[10].Position - (Vector3)vertices[9].Position;
            vectorB = (Vector3)vertices[11].Position - (Vector3)vertices[9].Position;

            //vectorC = (new Vector3(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y, vectorA.Z * vectorB.X - vectorA.X * vectorB.Z, vectorA.X * vectorB.Y - vectorA.Y * vectorB.X)) / 4.0f;

            vectorC = Vector3.Cross(vectorA, vectorB);

            vectorC.Normalize();

            vector4C = new Vector4(-vectorC.X, -vectorC.Y, -vectorC.Z, 1.0f);

            vertices[9].Normal = vector4C;
            vertices[10].Normal = vector4C;
            vertices[11].Normal = vector4C;

            

            Vector3 vertA, vertB, vertC;

            vertA = new Vector3(1.0f, -1.0f, 1.0f);
            vertB = new Vector3(0.0f, -1.0f, -1.0f);
            vertC = new Vector3(-1.0f, -1.0f, 1.0f);

            Vector3 vectA, vectB, vectC;

            vectA = vertB - vertA;
            vectB = vertC - vertA;

            vectC = Vector3.Cross(vectA, vectB);

            //vectC = (new Vector3(vectA.Y * vectB.Z - vectA.Z * vectB.Y, vectA.Z * vectB.X - vectA.X * vectB.Z, vectA.X * vectB.Y - vectA.Y * vectB.X));

            vectC.Normalize();*/





            uint[] indices = new uint[12]
            {
               0, 1, 2,
               3, 5, 4,
               6, 7, 8,
               9, 10, 11

            };


            return new MeshObject(_directX3DGraphics, position, yaw, pitch, roll, vertices, indices);
        }


        public MeshObject MakeTetrahedron(Vector4 position, float yaw, float pitch, float roll, ref BoundingBox collider)
        {
            MeshObject tetrahedron = MakeTetrahedron(position, yaw, pitch, roll);

            collider = new BoundingBox(new Vector3(position.X, position.Y, position.Z) + new Vector3(-1.0f, -1.0f, -1.0f), new Vector3(position.X, position.Y, position.Z) + new Vector3(1.0f, 1.0f, 1.0f));

            return tetrahedron;
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _imagingFactory);
        }
    }
}
