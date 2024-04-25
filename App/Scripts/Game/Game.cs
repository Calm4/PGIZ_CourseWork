using System;
using System.Collections.Generic;
using System.IO;
using Lab01.App.Scripts.DirectX;
using Lab01.App.Scripts.Environment;
using Lab01.App.Scripts.Textures;
using ObjLoader.Loader.Loaders;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Lab01.App.Scripts.Game
{
    class Game : IDisposable
    {
        RenderForm _renderForm;

        private const int NUM_LIGHTS = 4;

        private Texture parallelepipedTexture;
        private Texture _sixGrannikTexture;
        private Texture _cylinderTexture;
        private Texture _plotTexture;
        private MeshObject _sixGrannik;
        private MeshObject _parallelepiped;
        private MeshObject _plot;
        private MeshObject _cylinder;

        private MeshObject[] _lights = new MeshObject[NUM_LIGHTS];
        private Camera _camera;

        private DirectX3DGraphics _directX3DGraphics;
        private Renderer _renderer;

        private Bitmap _playerBitmap;

        private DeviceContext _d2dContext;

        private Bitmap1 d2dTarget;

        private SolidColorBrush _greenBrush;

        private SolidColorBrush _redBrush;

        private SolidColorBrush _blueBrush;

        private SolidColorBrush _purpleBrush;

        private SolidColorBrush _whiteBrush;

        private SolidColorBrush _secondGreenBrush;

        private SolidColorBrush _blackBrush;

        private SolidColorBrush _secondBlackBrush;

        private SolidColorBrush _danilkaBrush;

        float _sixGrannikSpeed = 0.0f;

        float _cylinderSpeed = 0.0f;

        const float g = 9.81f;

        private MaterialProperties _defaultMaterial;

        private MaterialProperties _floorMaterial;

        private MaterialProperties _blackMaterial;

        private MaterialProperties _icosahedronMaterial;

        private MaterialProperties _currentTetrahedronMaterial;
        private MaterialProperties _currentIcosahedronMaterial;

        private MaterialProperties _contactingMaterial;

        private MaterialProperties _currentIcosahedronColliderMaterial;
        private MaterialProperties _currentTetrahedronColliderMaterial;


        private MaterialProperties _rayMaterial;

        private LightProperties _light;

        private bool _isMap = false;

        private BoundingBox _playerCollider;

        private BoundingBox _sixGrannikCollider;

        private BoundingBox _plotCollider;

        private BoundingSphere _cylinderCollider;
        private BoundingBox _parallelepipedCollider;

        private Ray _cameraRay;

        private bool _firstRun = true;

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

        Vector2 _plotSize;


        TimeHelper _timeHelper;

        DXInput _dxInput;

        Loader _loader;

        public Game()
        {
            InvokeInitializers();

            /*InitializeGame();

            InitializeMaterials();


            CreateColliders(_loader);

            CreateLights();


            InitializeGameObjects(_loader);
           

            InitializeBrushes();


            DisposeLoader(_loader);*/
            
        }
        private void InvokeInitializers()
        {
            InitializeGame();
            InitializeMaterials();
            CreateLights();

            InitializeGameObjects(_loader);
            CreateColliders(_loader);
            InitializeBrushes();


            DisposeLoader(_loader);
        }
        private void InitializeGame()
        {
            _renderForm = new RenderForm();

            _renderForm.UserResized += RenderFormResizedCallback;
            _directX3DGraphics = new DirectX3DGraphics(_renderForm);
            _renderer = new Renderer(_directX3DGraphics);
            _renderer.CreateConstantBuffers();
            _loader = new Loader(_directX3DGraphics);
        }

        private void DisposeLoader(Loader loader)
        {
            loader.Dispose();
            loader = null;
            _dxInput = new DXInput(_renderForm.Handle);
        }

        private void InitializeGameObjects(Loader loader)
        {
            

            _plotSize = new Vector2(100.0f, 100.0f);
            _sixGrannikTexture = loader.LoadTextureFromFile("App/GameObjects/Images/6grannik.png", _renderer.AnisotropicSampler);
            _cylinderTexture = loader.LoadTextureFromFile("App/GameObjects/Images/CocaCola.png", _renderer.AnisotropicSampler);
            _plotTexture = loader.LoadTextureFromFile("App/GameObjects/Images/plotTexture.png", _renderer.AnisotropicSampler);
            parallelepipedTexture = loader.LoadTextureFromFile("App/GameObjects/Images/6grannik.png", _renderer.AnisotropicSampler);
           

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            var fileStream = new FileStream("6grannik.obj", FileMode.Open);
            var firstResult = objLoader.Load(fileStream);

            _sixGrannik = loader.LoadMeshObjectFromObjFile(firstResult, new Vector4(0.0f, 2.0f, 0.5f, 1.0f), 0f, 0f,
    0.0f, ref _sixGrannikTexture, _renderer.AnisotropicSampler);
          

            _plot = loader.MakePlot(new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 0.0f, 0.0f, _plotSize.X, _plotSize.Y,
                0f, ref _plotCollider);


            


            var objLoaderFactory2 = new ObjLoaderFactory();
            var objLoader2 = objLoaderFactory2.Create();

            var secondFileStream = new FileStream("cylinder.obj", FileMode.Open);
            var secondResult = objLoader2.Load(secondFileStream);

            _cylinder = loader.LoadMeshObjectFromObjFile(secondResult, new Vector4(4f, 2.0f, 0.0f, 1.0f), 0f, 0f,
                0.0f, ref _cylinderTexture, _renderer.AnisotropicSampler);

            var objLoaderFactory3 = new ObjLoaderFactory();
            var objLoader3 = objLoaderFactory3.Create();

            var thirdFileStream = new FileStream("66grannik.obj", FileMode.Open);
            var thirdResult = objLoader3.Load(thirdFileStream);

            _parallelepiped = loader.LoadMeshObjectFromObjFile(thirdResult, new Vector4(0f, 1.0f, 5, 1.0f), 0f, 0f,
0.0f, ref parallelepipedTexture, _renderer.AnisotropicSampler);

            _camera = new Camera(new Vector4(0.0f, 1.8f, -5.0f, 1.0f));
            _timeHelper = new TimeHelper();

            _cameraRay = new Ray(new Vector3(_camera.Position.X, _camera.Position.Y, _camera.Position.Z),
              _camera.GetViewTo());

        }
        private void CreateColliders(Loader loader)
        {
            _sixGrannikCollider = new BoundingBox(new Vector3(-1f, 0.0f, 4.5f), new Vector3(1f, 2.0f, 6.5f));
            _playerCollider = new BoundingBox(new Vector3(-0.25f, 0f, -0.25f), new Vector3(0.25f, 1.8f, 0.25f));
            _cylinderCollider = new BoundingSphere(new Vector3(_cylinder.Position.X, _cylinder.Position.Y, _cylinder.Position.Z), 1.0f);
            _parallelepipedCollider = new BoundingBox(new Vector3(-0.5f, 0.0f, 9.5f), new Vector3(0.5f, 2.0f, 19.5f));

            _colliders.Add(loader.MakeBoxCollider(_sixGrannikCollider, new Vector4(0.0f, 1.0f, -5.0f, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_plotCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeSphereCollider(_cylinderCollider, 0.0f, 0.0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_playerCollider, new Vector4(0f, 0f, 2f, 1.0f), 0f, 0f, 0.0f));

            _colliders.Add(loader.MakeBoxCollider(_parallelepipedCollider, new Vector4(_parallelepiped.Position.X, _parallelepiped.Position.Y, _parallelepiped.Position.Z, 1.0f), 0f, 0f, 0.0f));
        }
        private void CreateLights()
        {
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

            _light.GlobalAmbient = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
        }

        private void RenderFormResizedCallback(object sender, EventArgs e)
        {
            _directX3DGraphics.Resize();
            _camera.Aspect = _renderForm.ClientSize.Width / (float)_renderForm.ClientSize.Height;
        }

      

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

            if (!_sixGrannikCollider.Intersects(ref _plotCollider))
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
            
            if (!_cameraRay.Intersects(ref _sixGrannikCollider))
            {
                _currentTetrahedronColliderMaterial = _blackMaterial;
            }
            else
            {
                _currentTetrahedronColliderMaterial = _rayMaterial;
            }

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


            Vector3 cameraMovement = InputMovement();

            //_camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);

            Vector3 proposedMovement = new Vector3(cameraMovement.X, 0, cameraMovement.Z);
            BoundingBox proposedPlayerCollider = new BoundingBox(_playerCollider.Minimum + proposedMovement, _playerCollider.Maximum + proposedMovement);

            if (!_sixGrannikCollider.Intersects(ref proposedPlayerCollider) && 
               (!_cylinderCollider.Intersects(ref proposedPlayerCollider)) && 
               (!_parallelepipedCollider.Intersects(ref proposedPlayerCollider)))
            {
                _camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _colliders[3].MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _playerCollider = proposedPlayerCollider;
            }
           /* if (!_cylinderCollider.Intersects(ref proposedPlayerCollider))
            {
                _camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _colliders[3].MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _playerCollider = proposedPlayerCollider;
            }

            if (!_parallelepipedCollider.Intersects(ref proposedPlayerCollider))
            {
                _camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _colliders[3].MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
                _playerCollider = proposedPlayerCollider;
            }*/



            _cameraRay.Position += cameraMovement;


            Vector3 tetrahedronMovement = Vector3.Zero;

            if (_dxInput.IsKeyPressed(Key.U))
            {
                _cylinder.MoveBy(0f, 0.5f, 0f);
                _colliders[2].MoveBy(0f, 0.5f, 0f);
                _cylinderCollider.Center += new Vector3(0f, 0.5f, 0f);
            }
            
        

            RenderObjects();
        }
        private Vector3 InputMovement()
        {
            Vector3 cameraMovement = Vector3.Zero;

            if (_dxInput.IsKeyPressed(Key.W))
            {
                cameraMovement += _camera.GetViewForward() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.S))
            {
                cameraMovement -= _camera.GetViewForward() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.A))
            {
                cameraMovement -= _camera.GetViewRight() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.D))
            {
                cameraMovement += _camera.GetViewRight() * 0.1f;
            }
            /*   if (_dxInput.IsKeyPressed(Key.Space))
               {
                   cameraMovement.Y += .1f;
               }

               if (_dxInput.IsKeyPressed(Key.LeftControl))
               {
                   cameraMovement.Y -= .1f;
               }*/

            //_camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);
            return cameraMovement;
        }
        private void InitializeBrushes()
        {
            var green = Color.Green;

            green.A = 100;

            var white = Color.WhiteSmoke;
            white.A = 70;

            var black = Color.Black;
            black.A = 100;

            var gg = new Color(244, 230, 203);
            gg.A = 128;

            _greenBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, green);
            _redBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, Color.Red);
            _blueBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, Color.Blue);
            _purpleBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, Color.Purple);
            _whiteBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, Color.WhiteSmoke);

            green = Color.Green;
            _secondGreenBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, green);
            _blackBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, black);
            _danilkaBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, gg);
            _secondBlackBrush = new SolidColorBrush(_directX3DGraphics.D2DRenderTarget, Color.Black);
        }
        private void InitializeMaterials()
        {
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
        }
        private void RenderObjects()
        {
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

            _renderer.SetPerObjectConstantBuffer(_currentTetrahedronColliderMaterial);
            _renderer.UpdatePerObjectConstantBuffers(_colliders[4].GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.RenderMeshObject(_colliders[4]);

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
        }

        private void CheckPositions()
        {
            var b = _sixGrannik.Position;
            var a = _camera.Position;
            var c = _sixGrannik.Position - _camera.Position;
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