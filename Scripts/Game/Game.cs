using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab01.App.Scripts.Game
{
    class Game : IDisposable
    {
        RenderForm _renderForm;

        private const int NUM_LIGHTS = 4;

        private Texture oboiTexture;
        private Texture oboiTexture2;
        private Texture _potolokTexture;
        private Texture _sixGrannikTexture;
        private Texture _cylinderTexture;
        private Texture _plotTexture;
        private Texture _playerTexture;
        private Texture _letterTexture;
        private Texture _carpetTexture;
        private Texture _yaschikTexture;
        private Texture _verxYaschikTexture;
        private MeshObject _sixGrannik;
        private MeshObject _parallelepiped1;
        private MeshObject _parallelepiped2;
        private MeshObject _parallelepiped3;
        private MeshObject _parallelepiped4;
        private MeshObject _plot;
        private MeshObject _potolok;
        private MeshObject _letter;
        private MeshObject _yaschik1;
        private MeshObject _yaschik2;
        private MeshObject _yaschik3;
        private MeshObject _yaschik4;
        private MeshObject _yaschik5;
        private MeshObject _verxYaschik1;
        private MeshObject _verxYaschik2;
        private MeshObject _verxYaschik3;
        private MeshObject _verxYaschik4;
        private MeshObject _verxYaschik5;
        private MeshObject _carpet;
        private MeshObject _cylinder;
        private MeshObject _player;

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
        private BoundingBox _potolokCollider;

        private BoundingSphere _cylinderCollider;
        private BoundingBox _parallelepipedCollider1;
        private BoundingBox _parallelepipedCollider2;
        private BoundingBox _parallelepipedCollider3;
        private BoundingBox _parallelepipedCollider4;
        private BoundingBox _yaschikCollider;
        private BoundingBox _verxYaschikCollider;

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
        Vector2 _potolokSize;


        TimeHelper _timeHelper;

        DXInput _dxInput;

        Loader _loader;

        public Game()
        {
            InvokeInitializers();
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

        private const string GameObjectsPath = "GameObjects/";
        private const string ImagesPath = GameObjectsPath + "Images/";
        private const string ObjectsPath = GameObjectsPath + "Objects/";

        private void InitializeGameObjects(Loader loader)
        {
            _sixGrannikTexture = LoadTexture(loader, "6grannik.png");
            _cylinderTexture = LoadTexture(loader, "CocaCola.png");
            _plotTexture = LoadTexture(loader, "plotTexture.png");
            oboiTexture = LoadTexture(loader, "oboi.jpg");
            oboiTexture2 = LoadTexture(loader, "oboi2.jpg");
            _playerTexture = LoadTexture(loader, "CocaCola.png");
            _potolokTexture = LoadTexture(loader, "potolok.jpg");
            _letterTexture = LoadTexture(loader, "letter.jpg");
            _carpetTexture = LoadTexture(loader, "carpet.jpg");
            _yaschikTexture = LoadTexture(loader, "yaschik.jpg");
            _verxYaschikTexture = LoadTexture(loader, "verx_yaschik.png");
            _plotSize = new Vector2(100.0f, 100.0f);
            _plot = loader.MakePlot(new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 0.0f, 0.0f, _plotSize.X, _plotSize.Y, 0f, ref _plotCollider);
            
            _player = LoadObjectWithCollider(loader, "player.obj", new Vector4(0f, 0f, 0f, 1f), ref _playerTexture, out _playerCollider);
            _sixGrannik = LoadObjectWithCollider(loader, "6grannik.obj", new Vector4(0.0f, 2.0f, 5f, 1.0f), ref _sixGrannikTexture, out _sixGrannikCollider);
            _cylinder = LoadObject(loader, "cylinder.obj", new Vector4(4f, 2.0f, 0.0f, 1.0f), ref _cylinderTexture);
            _parallelepiped1 = LoadObjectWithCollider(loader, "oboi1.obj", new Vector4(0f, 0f, 20f, 1.0f), ref oboiTexture, out _parallelepipedCollider1);
            _parallelepiped2 = LoadObjectWithCollider(loader, "oboi2.obj", new Vector4(0f, 0f, -20f, 1.0f), ref oboiTexture, out _parallelepipedCollider2);
            _parallelepiped3 = LoadObjectWithCollider(loader, "oboi_p1.obj", new Vector4(-20f, 0f, 0f, 1.0f), ref oboiTexture, out _parallelepipedCollider3);
            _parallelepiped4 = LoadObjectWithCollider(loader, "oboi_type2.obj", new Vector4(20f, 0f, 0f, 1.0f), ref oboiTexture, out _parallelepipedCollider4);
            _potolok = LoadObject(loader, "potolok2.obj", new Vector4(0f, 4f, 0f, 1.0f), ref _potolokTexture);
            _letter = LoadObject(loader, "letterMini.obj", new Vector4(1f, 0.5f, 0f, 1.0f), ref _letterTexture);
            _carpet = LoadObject(loader, "carpet.obj", new Vector4(10f, 2f, -19f, 1.0f), ref _carpetTexture);
            _yaschik1 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -17f, 1.0f), ref _yaschikTexture, out _yaschikCollider);
            _yaschik2 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -15.4f, 1.0f), ref _yaschikTexture, out _yaschikCollider);
            _yaschik3 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -13.8f, 1.0f), ref _yaschikTexture, out _yaschikCollider);
            _yaschik4 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -12.2f, 1.0f), ref _yaschikTexture, out _yaschikCollider);
            _yaschik5 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -10.4f, 1.0f), ref _yaschikTexture, out _yaschikCollider);
            _verxYaschik1 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -17f, 1.0f), ref _verxYaschikTexture, out _yaschikCollider);
            _verxYaschik2 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -15.4f, 1.0f), ref _verxYaschikTexture, out _yaschikCollider);
            _verxYaschik3 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -13.8f, 1.0f), ref _verxYaschikTexture, out _yaschikCollider);
            _verxYaschik4 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -12.2f, 1.0f), ref _verxYaschikTexture, out _yaschikCollider);
            _verxYaschik5 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -10.4f, 1.0f), ref _verxYaschikTexture, out _yaschikCollider);

            _camera = new Camera(new Vector4(_player.Position.X, 1.8f, _player.Position.Z, 1.0f));
            _timeHelper = new TimeHelper();
            _cameraRay = new Ray(new Vector3(_camera.Position.X, _camera.Position.Y, _camera.Position.Z), _camera.GetViewTo());
        }

        private Texture LoadTexture(Loader loader, string fileName)
        {
            return loader.LoadTextureFromFile(ImagesPath + fileName, _renderer.AnisotropicSampler);
        }

        private MeshObject LoadObjectWithCollider(Loader loader, string fileName, Vector4 position, ref Texture texture, out BoundingBox collider)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var fileStream = new FileStream(ObjectsPath + fileName, FileMode.Open);
            var result = objLoader.Load(fileStream);

            var meshObject = loader.LoadMeshObjectFromObjFile(result, position, 0f, 0f, 0f, ref texture, _renderer.AnisotropicSampler);
            collider = new BoundingBox(
                new Vector3(result.Vertices.Min(v => v.X), result.Vertices.Min(v => v.Y), result.Vertices.Min(v => v.Z)) + (Vector3)meshObject.Position,
                new Vector3(result.Vertices.Max(v => v.X), result.Vertices.Max(v => v.Y), result.Vertices.Max(v => v.Z)) + (Vector3)meshObject.Position);
            fileStream.Close();

            return meshObject;
        }
        private MeshObject LoadObject(Loader loader, string fileName, Vector4 position, ref Texture texture)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var fileStream = new FileStream(ObjectsPath + fileName, FileMode.Open);
            var result = objLoader.Load(fileStream);

            var meshObject = loader.LoadMeshObjectFromObjFile(result, position, 0f, 0f, 0f, ref texture, _renderer.AnisotropicSampler);
            fileStream.Close();

            return meshObject;
        }

        private void CreateColliders(Loader loader)
        {
            //_sixGrannikCollider = new BoundingBox(new Vector3(-1f, 0.0f, 4.5f), new Vector3(1f, 2.0f, 6.5f));
            //_playerCollider = new BoundingBox(new Vector3(-0.25f, 0f, -0.25f), new Vector3(0.25f, 1.8f, 0.25f));
            _cylinderCollider = new BoundingSphere(new Vector3(_cylinder.Position.X, _cylinder.Position.Y, _cylinder.Position.Z), 1.0f);


            _colliders.Add(loader.MakeBoxCollider(_sixGrannikCollider, new Vector4(_sixGrannik.Position.X, _sixGrannik.Position.Y, _sixGrannik.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_plotCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeSphereCollider(_cylinderCollider, 0.0f, 0.0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_playerCollider, new Vector4(0f, 0f, 0f, 1.0f), 0f, 0f, 0.0f));

            _colliders.Add(loader.MakeBoxCollider(_parallelepipedCollider1, new Vector4(_parallelepiped1.Position.X, _parallelepiped1.Position.Y, _parallelepiped1.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_parallelepipedCollider2, new Vector4(_parallelepiped2.Position.X, _parallelepiped2.Position.Y, _parallelepiped2.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_parallelepipedCollider3, new Vector4(_parallelepiped3.Position.X, _parallelepiped3.Position.Y, _parallelepiped3.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_potolokCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
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


            Vector3 playerMovement = InputMovement();

            //_camera.MoveBy(cameraMovement.X, cameraMovement.Y, cameraMovement.Z);


            // Попытка перемещения
            Vector3 attemptedMovement = playerMovement;
            _playerCollider.Minimum += attemptedMovement;
            _playerCollider.Maximum += attemptedMovement;

            // Проверка на столкновение
            if (_sixGrannikCollider.Intersects(ref _playerCollider) ||
                _cylinderCollider.Intersects(ref _playerCollider) ||
                _parallelepipedCollider1.Intersects(ref _playerCollider) ||
                _parallelepipedCollider2.Intersects(ref _playerCollider) ||
                _parallelepipedCollider3.Intersects(ref _playerCollider) ||
                _parallelepipedCollider4.Intersects(ref _playerCollider) ||
                _yaschikCollider.Intersects(ref _playerCollider))
            {
                // Если произошло столкновение, отменяем перемещение
                _playerCollider.Minimum -= attemptedMovement;
                _playerCollider.Maximum -= attemptedMovement;
            }
            else
            {
                // Если столкновения нет, применяем перемещение
                _camera.MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);
                _colliders[3].MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);
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



            _cameraRay.Position += playerMovement;


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
            Vector3 playerMovement = Vector3.Zero;

            if (_dxInput.IsKeyPressed(Key.W))
            {
                playerMovement += _camera.GetViewForward() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.S))
            {
                playerMovement -= _camera.GetViewForward() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.A))
            {
                playerMovement -= _camera.GetViewRight() * 0.1f;
            }

            if (_dxInput.IsKeyPressed(Key.D))
            {
                playerMovement += _camera.GetViewRight() * 0.1f;
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
            return playerMovement;
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

        private void RenderObject(Matrix viewMatrix, Matrix projectionMatrix, MeshObject meshObject, Texture objectTexture)
        {
            _renderer.SetPerObjectConstantBuffer(_currentTetrahedronMaterial);
            _renderer.UpdatePerObjectConstantBuffers(meshObject.GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.SetTexture(objectTexture);
            _renderer.RenderMeshObject(meshObject);
        }
        private void RenderObjects()
        {
            Matrix viewMatrix = _camera.GetViewMatrix();
            Matrix projectionMatrix = _camera.GetProjectionMatrix();
            _light.EyePosition = _camera.Position;

            _renderer.BeginRender();

            _renderer.SetLightConstantBuffer(_light);


            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Wireframe);

            for (int i = 0; i < _colliders.Count; i++)
            {
                _renderer.SetPerObjectConstantBuffer(_currentIcosahedronColliderMaterial);
                _renderer.UpdatePerObjectConstantBuffers(_colliders[i].GetWorldMatrix(), viewMatrix, projectionMatrix);
                _renderer.RenderMeshObject(_colliders[i]);
            }

            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Solid);

            RenderObject(viewMatrix, projectionMatrix, _sixGrannik, _sixGrannikTexture);
            RenderObject(viewMatrix, projectionMatrix, _cylinder, _cylinderTexture);
            RenderObject(viewMatrix, projectionMatrix, _plot, _plotTexture);
            RenderObject(viewMatrix, projectionMatrix, _parallelepiped1, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _parallelepiped2, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _parallelepiped3, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _parallelepiped4, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _potolok, _potolokTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _carpet, _carpetTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik1, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik3, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik4, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik5, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik1, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik2, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik3, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik4, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik5, _verxYaschikTexture);

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
            _potolok.Dispose();
            _renderer.Dispose();
            _directX3DGraphics.Dispose();
        }
    }
}