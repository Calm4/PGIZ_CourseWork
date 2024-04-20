using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.DirectInput;
using SharpDX;
using SharpDX.Windows;
using Lab01;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System.Threading;
using SharpDX.Mathematics.Interop;
using SharpDX.DirectWrite;
using SharpDX.IO;
using SharpDX.WIC;
using System.IO.Packaging;
using ObjLoader.Loader.Loaders;
using System.IO;

namespace Lab02
{
    class Game : IDisposable
    {
        RenderForm _renderForm;

        const int NUM_LIGHTS = 4;

        Texture _sixGrannikTexture;
        Texture _cylinderTexture;
        Texture _plotTexture;
        MeshObject _sixGrannik;
        MeshObject _plot;
        MeshObject _cylinder;

        MeshObject[] _lights = new MeshObject[NUM_LIGHTS];
        Camera _camera;

        DirectX3DGraphics _directX3DGraphics;
        Renderer _renderer;

        SharpDX.Direct2D1.Bitmap _playerBitmap;

        SharpDX.Direct2D1.DeviceContext _d2dContext;

        private SharpDX.Direct2D1.Bitmap1 d2dTarget;

        SharpDX.Direct2D1.SolidColorBrush _greenBrush;

        SharpDX.Direct2D1.SolidColorBrush _redBrush;

        SharpDX.Direct2D1.SolidColorBrush _blueBrush;

        SharpDX.Direct2D1.SolidColorBrush _purpleBrush;

        SharpDX.Direct2D1.SolidColorBrush _whiteBrush;

        SharpDX.Direct2D1.SolidColorBrush _secondGreenBrush;

        SharpDX.Direct2D1.SolidColorBrush _blackBrush;

        SharpDX.Direct2D1.SolidColorBrush _secondBlackBrush;
        
        SharpDX.Direct2D1.SolidColorBrush _danilkaBrush;

        float _sixGrannikSpeed = 0.0f;

        float _cylinderSpeed = 0.0f;

        const float g = 9.81f;

        MaterialProperties _defaultMaterial;

        MaterialProperties _floorMaterial;

        MaterialProperties _blackMaterial;

        MaterialProperties _icosahedronMaterial;

        MaterialProperties _currentTetrahedronMaterial;
        MaterialProperties _currentIcosahedronMaterial;

        MaterialProperties _contactingMaterial;

        MaterialProperties _currentIcosahedronColliderMaterial;
        MaterialProperties _currentTetrahedronColliderMaterial;


        MaterialProperties _rayMaterial;

        LightProperties _light;

        bool _isMap = false;

        BoundingBox _sixGrannikCollider;

        BoundingBox _plotCollider;

        BoundingSphere _cylinderCollider;

        Ray _cameraRay;

        Vector4[] _lightColors = new Vector4[NUM_LIGHTS]
        {
            new Vector4(0f, 1f, 1f, 1f),
            new Vector4(0f, 1f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(0.5f, 0f, 0f, 1f)
        };

        int[] _lighTypes = new int[NUM_LIGHTS]
        {
            1,
            0,
            0,
            1,
        };

        int[] _lightEnabled = new int[NUM_LIGHTS]
        {
            1,
            1,
            1,
            1
        };

        List<MeshObject> _colliders = new List<MeshObject>();

        MeshObject _cuban;

        Vector2 _plotSize;


        TimeHelper _timeHelper;

        DXInput _dxInput;

        public Game()
        {
            _renderForm = new RenderForm();
            
            _renderForm.UserResized += RenderFormResizedCallback;
            _directX3DGraphics = new DirectX3DGraphics(_renderForm);
            _renderer = new Renderer(_directX3DGraphics);
            _renderer.CreateConstantBuffers();
            _defaultMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(0f, 0.0f, 0.0f, 1f),
                    Ambient = new Vector4(0f, 0.1f, 0.06f, 1.0f),
                    Diffuse = new Vector4(0f, 0.50980392f, 0.50980392f, 1f),
                    Specular = new Vector4(0.50196078f, 0.50196078f, 0.50196078f, 1f),
                    SpecularPower = 32f,
                    UseTexture = 1
                }
            };

            _floorMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(0.25f, 0.25f, 0.25f, 1f),
                    Ambient = new Vector4(0.5f, 0.5f, 0.5f, 1f),
                    Diffuse = new Vector4(0.5f, 0.5f, 0.4f, 1.0f),
                    Specular = new Vector4(0.7f, 0.7f, 0.04f, 1f),
                    SpecularPower = 10.0f,
                    UseTexture = 0
                }
            };



            _blackMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(0.0f, 0.0f, 0.0f, 1f),
                    Ambient = new Vector4(0.5f, 0.5f, 0.5f, 1f),
                    Diffuse = new Vector4(0.5f, 0.5f, 0.4f, 1.0f),
                    Specular = new Vector4(0.7f, 0.7f, 0.04f, 1f),
                    SpecularPower = 10.0f,
                    UseTexture = 0
                }
            };

            _icosahedronMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(0.2f, 0.2f, 0.2f, 1f),
                    Ambient = new Vector4(0.19225f, 0.19225f, 0.19225f, 1f),
                    Diffuse = new Vector4(0.50754f, 0.50754f, 0.50754f, 1.0f),
                    Specular = new Vector4(0.508273f, 0.508273f, 0.508273f, 1f),
                    SpecularPower = 51.2f,
                    UseTexture = 0
                }
            };

            _contactingMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(1.0f, 0.0f, 0.0f, 1f),
                    Ambient = new Vector4(0.19225f, 0.19225f, 0.19225f, 1f),
                    Diffuse = new Vector4(0.50754f, 0.50754f, 0.50754f, 1.0f),
                    Specular = new Vector4(0.508273f, 0.508273f, 0.508273f, 1f),
                    SpecularPower = 51.2f,
                    UseTexture = 1
                }
            };

            _rayMaterial = new MaterialProperties
            {
                Material = new Material
                {
                    Emmisive = new Vector4(1.0f, 1.0f, 0.0f, 1f),
                    Ambient = new Vector4(0.19225f, 0.19225f, 0.19225f, 1f),
                    Diffuse = new Vector4(0.50754f, 0.50754f, 0.50754f, 1.0f),
                    Specular = new Vector4(0.508273f, 0.508273f, 0.508273f, 1f),
                    SpecularPower = 51.2f,
                    UseTexture = 0
                }
            };

            _currentTetrahedronMaterial = _defaultMaterial;

            _currentIcosahedronMaterial = _icosahedronMaterial;

            _currentIcosahedronColliderMaterial = _floorMaterial;

            _currentTetrahedronColliderMaterial = _floorMaterial;

            _sixGrannikCollider = new BoundingBox();

            _cylinderCollider = new BoundingSphere();
            


            _light.Lights = new Light[NUM_LIGHTS];
            for (int i = 0; i < NUM_LIGHTS; i++)
            {
                Light light = new Light();
                light.Enabled = _lightEnabled[i];
                light.LightType = _lighTypes[i];
                light.Color = _lightColors[i];
                light.SpotAngle = 0.785398f;
                light.ConstantAttenuation = 1.0f;
                light.LinearAttenuation = 0.08f;
                light.QuadraticAttenuation = 0.0f;
                light.Position = new Vector4((float)i * 5f - 5f, -9.5f, 0f, 1f);
                light.Direction = new Vector4(-light.Position.X, -light.Position.Y, -light.Position.Y, 0.0f);
                light.Direction.Normalize();

                _light.Lights[i] = light;
            }

            _plotSize = new Vector2(100.0f, 100.0f);

            _light.GlobalAmbient = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);

            Loader loader = new Loader(_directX3DGraphics);

            _sixGrannikTexture = loader.LoadTextureFromFile("6grannik.png", _renderer.AnisotropicSampler);
            _cylinderTexture = loader.LoadTextureFromFile("CocaCola.png", _renderer.AnisotropicSampler);

            _plotTexture = loader.LoadTextureFromFile("plotTexture.png", _renderer.AnisotropicSampler);

            // -------------------------------------------------------------------------------

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            var fileStream = new FileStream("6grannik.obj", FileMode.Open);
            var firstResult = objLoader.Load(fileStream);

            _sixGrannik = loader.LoadMeshObjectFromObjFile(firstResult, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f, ref _sixGrannikTexture, _renderer.AnisotropicSampler);
            _sixGrannikCollider = new BoundingBox( new Vector3(-1.0f, -1.0f, -1.0f), new Vector3(1.0f, 1.0f, 1.0f)); 
            _cuban = loader.MakeCube(new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f);
            _colliders.Add(loader.MakeBoxCollider(_sixGrannikCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
            _plot = loader.MakePlot(new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 0.0f, 0.0f, _plotSize.X, _plotSize.Y, -10f, ref _plotCollider);

            // -------------------------------------------------------------------------------

            var objLoaderFactory2 = new ObjLoaderFactory();
            var objLoader2 = objLoaderFactory2.Create();

            var secondFileStream = new FileStream("cylinder.obj", FileMode.Open);
            var secondResult = objLoader2.Load(secondFileStream);

            _cylinder = loader.LoadMeshObjectFromObjFile(secondResult, new Vector4(3.5f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f, ref _cylinderTexture, _renderer.AnisotropicSampler);
            _cylinderCollider = new BoundingSphere(new Vector3(_cylinder.Position.X, _cylinder.Position.Y, _cylinder.Position.Z), 1.0f);
            _colliders.Add(loader.MakeBoxCollider(_plotCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeSphereCollider(_cylinderCollider, 0.0f, 0.0f, 0.0f));
            
            _camera = new Camera(new Vector4(0.0f, 2.0f, -10.0f, 1.0f));
            _timeHelper = new TimeHelper();



            _cameraRay = new Ray(new Vector3(_camera.Position.X, _camera.Position.Y, _camera.Position.Z), _camera.GetViewTo());

            //_blackBrush = new SharpDX.Direct2D1.SolidColorBrush(_directX3DGraphics.D2dContext, SharpDX.Color.Black);

            var green = SharpDX.Color.Green;

            green.A = 100;

            var white = SharpDX.Color.WhiteSmoke;
            white.A = 70;

            var black = SharpDX.Color.Black;
            black.A = 100;

            var gg = new SharpDX.Color(244, 230, 203);
            gg.A = 128;

            _greenBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, green);
            _redBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, SharpDX.Color.Red);
            _blueBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, SharpDX.Color.Blue);
            _purpleBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, SharpDX.Color.Purple);
            _whiteBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, SharpDX.Color.WhiteSmoke);

            green = SharpDX.Color.Green;
            _secondGreenBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, green);
            _blackBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, black);
            _danilkaBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, gg);
            _secondBlackBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, SharpDX.Color.Black);

            //_playerBitmap = DirectX3DGraphics.LoadFromFile(_directX3DGraphics.D2DRenderTarget, "textureTetrahedron.png");

            loader.Dispose();
            loader = null;
            _dxInput = new DXInput(_renderForm.Handle);
        }

        private void RenderFormResizedCallback(object sender, EventArgs e)
        {
            _directX3DGraphics.Resize();
            _camera.Aspect = _renderForm.ClientSize.Width / (float)_renderForm.ClientSize.Height;
        }

        private bool _firstRun = true;

        public void RenderLoopCallBack()
        {
            if (_firstRun)
            {
                RenderFormResizedCallback(this, EventArgs.Empty);
                _firstRun = false;
            }
            _timeHelper.Update();
            _renderForm.Text = "FPS: " + _timeHelper.FPS.ToString();

            _sixGrannikSpeed = _sixGrannikSpeed - g * _timeHelper.DeltaT;

            if(!_sixGrannikCollider.Intersects(ref _plotCollider))
            {

                _sixGrannik.MoveBy(0f, _sixGrannikSpeed * _timeHelper.DeltaT, 0f);
                _colliders[0].MoveBy(0f, _sixGrannikSpeed * _timeHelper.DeltaT, 0f);
                _sixGrannikCollider.Maximum += new Vector3(0f, _sixGrannikSpeed * _timeHelper.DeltaT, 0f);
                _sixGrannikCollider.Minimum += new Vector3(0f, _sixGrannikSpeed * _timeHelper.DeltaT, 0f);
            }
            else
            {
                _currentTetrahedronMaterial = _contactingMaterial;
            }


            _cylinderSpeed = _cylinderSpeed - g * _timeHelper.DeltaT;

            if (!_cylinderCollider.Intersects(ref _plotCollider))
            {
                _cylinder.MoveBy(0f, _cylinderSpeed * _timeHelper.DeltaT, 0f);
                _colliders[2].MoveBy(0f, _cylinderSpeed * _timeHelper.DeltaT, 0f);
                _cylinderCollider.Center += new Vector3(0f, _cylinderSpeed * _timeHelper.DeltaT, 0f);
            }
            else
            {
                _currentIcosahedronMaterial = _contactingMaterial;
            }

            /*if(!_cameraRay.Intersects(ref _cylinderCollider))
            {
                _currentIcosahedronColliderMaterial = _blackMaterial; 
            }
            else
            {
                _currentIcosahedronColliderMaterial = _rayMaterial;
            }*/

            if (!_cameraRay.Intersects(ref _sixGrannikCollider))
            {
                _currentTetrahedronColliderMaterial = _blackMaterial; 
            }
            else
            {
                _currentTetrahedronColliderMaterial = _rayMaterial;
            }

            _dxInput.Update();

            _camera.YawBy(_dxInput.GetMouseDeltaX() * 0.001f);
            _camera.PitchBy(_dxInput.GetMouseDeltaY() * 0.001f);

            _cameraRay.Direction = _camera.GetViewTo();


            Vector3 cameraMovement = Vector3.Zero;
            if (_dxInput.IsKeyPressed(Key.W))
            {
                cameraMovement += _camera.GetViewTo() * 0.1f;
            }
            if (_dxInput.IsKeyPressed(Key.S))
            {
                cameraMovement -= _camera.GetViewTo() * 0.1f;
            }
            if (_dxInput.IsKeyPressed(Key.A))
            {
                cameraMovement -= _camera.GetViewRight() * 0.1f;
            }
            if (_dxInput.IsKeyPressed(Key.D))
            {

                cameraMovement += _camera.GetViewRight() * 0.1f;
            }
            if (_dxInput.IsKeyPressed(Key.Space))
            {
                cameraMovement.Y += .1f;
            }
            if (_dxInput.IsKeyPressed(Key.LeftControl))
            {
                cameraMovement.Y -= .1f;
            }
            
            _camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);

            _cameraRay.Position += cameraMovement;


            Vector3 tetrahedronMovement = Vector3.Zero;

            if (_dxInput.IsKeyPressed(Key.Up))
            {
                tetrahedronMovement.Z += .1f;
            }
            if (_dxInput.IsKeyPressed(Key.Down))
            {
                tetrahedronMovement.Z -= .1f;
            }
            if (_dxInput.IsKeyPressed(Key.Left))
            {
                tetrahedronMovement.X -= .1f;
            }
            if (_dxInput.IsKeyPressed(Key.Right))
            {
                tetrahedronMovement.X += .1f;
            }

            _sixGrannik.MoveBy(tetrahedronMovement.X, tetrahedronMovement.Y, tetrahedronMovement.Z);


            if (_dxInput.IsKeyPressed(Key.U))
            {
                _cylinder.MoveBy(0f, 0.5f, 0f);
                _colliders[2].MoveBy(0f, 0.5f, 0f);
                _cylinderCollider.Center += new Vector3(0f, 0.5f, 0f);
            }


            PressKeyboard();
            Matrix viewMatrix = _camera.GetViewMatrix();
            Matrix projectionMatrix = _camera.GetProjectionMatrix();
            _light.EyePosition = _camera.Position;
            _renderer.BeginRender();

            _renderer.SetLightConstantBuffer(_light);


            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Wireframe);


            _renderer.SetPerObjectConstantBuffer(_currentIcosahedronColliderMaterial); 
            _renderer.UpdatePerObjectConstantBuffers(_colliders[2].GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.RenderMeshObject(_colliders[2]);

            _renderer.SetPerObjectConstantBuffer(_currentTetrahedronColliderMaterial); 
            _renderer.UpdatePerObjectConstantBuffers(_colliders[0].GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.RenderMeshObject(_colliders[0]);

            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Solid);


            _renderer.SetPerObjectConstantBuffer(_currentTetrahedronMaterial);
            _renderer.UpdatePerObjectConstantBuffers(_sixGrannik.GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.SetTexture(_sixGrannikTexture);
            _renderer.RenderMeshObject(_sixGrannik);



            


            _renderer.SetPerObjectConstantBuffer(_floorMaterial);
            _renderer.UpdatePerObjectConstantBuffers(_plot.GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.SetTexture(_plotTexture);
            _renderer.RenderMeshObject(_plot);


            _renderer.SetPerObjectConstantBuffer(_currentIcosahedronMaterial);
            _renderer.UpdatePerObjectConstantBuffers(_cylinder.GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.SetTexture(_cylinderTexture);
            _renderer.RenderMeshObject(_cylinder);

            _renderer.EndRender();

            if (_dxInput.IsKeyReleased(Key.M))
            {
                _isMap = !_isMap;
            }

            if (_dxInput.IsKeyReleased(Key.O))
            {
                CheckPositions();
            }



            TextFormat testTextFormat = new TextFormat(_directX3DGraphics.FactoryDWrite, "Calibri", 28)
            {
                TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                ParagraphAlignment = ParagraphAlignment.Center,
            };

            _directX3DGraphics.D2DRenderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;

            _directX3DGraphics.D2DRenderTarget.BeginDraw();

            _directX3DGraphics.D2DRenderTarget.EndDraw();
            _directX3DGraphics.SwapChain.Present(0, PresentFlags.None);


            //_directX3DGraphics.D2dContext.DrawRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(
            //    _renderForm.Left + 100.0f, _renderForm.Top + 100.0f, _renderForm.Right - 100.0f, _renderForm.Bottom - 100.0f), _blackBrush);
            // _directX3DGraphics.D2dContext.EndDraw();
        }

        private void CheckPositions()
        {
            var b = _sixGrannik.Position;
            var a = _camera.Position;
            var c = _sixGrannik.Position - _camera.Position;
        }

        private void PressKeyboard()
        {
            /* Vector3 tetrahedronMovement = Vector3.Zero;
            if(_dxInput.IsKeyPressed(Key.W))
            {
                tetrahedronMovement.Z += -.1f;
            }
            if(_dxInput.IsKeyPressed(Key.S))
            {
                tetrahedronMovement.Z += .1f;
            }
            if(_dxInput.IsKeyPressed(Key.A))
            {
                tetrahedronMovement.X += -.1f;
            }
            if(_dxInput.IsKeyPressed(Key.D))
            {
                tetrahedronMovement.X += .1f;
            }

            _tetrahedron.MoveBy(tetrahedronMovement.X, tetrahedronMovement.Y, tetrahedronMovement.Z);*/

            /*Vector3 tetrahedronMovement = Vector3.Zero;
            if (_dxInput.IsKeyReleased(Key.W))
            {
                tetrahedronMovement += _tetrahedron.GetForwardVector() * 1f;
            }
            if (_dxInput.IsKeyReleased(Key.S))
            {
                tetrahedronMovement -= _tetrahedron.GetForwardVector() * 1f;
            }
            if (_dxInput.IsKeyReleased(Key.A))
            {
                tetrahedronMovement += _tetrahedron.GetRightVector() * 1f;
            }
            if (_dxInput.IsKeyReleased(Key.D))
            {
                tetrahedronMovement -= _tetrahedron.GetRightVector() * 1f;
            }

            _tetrahedron.MoveBy(tetrahedronMovement.X, tetrahedronMovement.Y, tetrahedronMovement.Z);*/
        }

        public void Run()
        {
            RenderLoop.Run(_renderForm, RenderLoopCallBack);
        }

        public void Dispose()
        {
            _sixGrannik.Dispose();
            _sixGrannikTexture.Dispose();
            _plot.Dispose();
            _renderer.Dispose();
            _directX3DGraphics.Dispose();
        }
    }
}
