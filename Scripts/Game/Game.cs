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
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab01.App.Scripts.Game
{
    class Game : IDisposable
    {
        RenderForm _renderForm;

        private const int NUM_LIGHTS = 4;

        private Texture oboiTexture;
        private Texture _stenaTexture;
        private Texture oboiTexture2;
        private Texture _potolokTexture;
        private Texture _sixGrannikTexture;
        private Texture _cylinderTexture;
        private Texture _plotTexture;
        private Texture _komodTexture;
        private Texture _playerTexture;
        private Texture _letterTexture;
        private Texture _divan1Texture;
        private Texture _divan2Texture;
        private Texture _xolodilnikTexture;
        private Texture _knifeTexture;
        private Texture _plitaTexture;
        private Texture _rakovinaTexture;
        private Texture _carpetTexture;
        private Texture _carpet2Texture;
        private Texture _carpet3Texture;
        private Texture _yaschikTexture;
        private Texture _yaschikPerpTexture;
        private Texture _verxYaschikTexture;
        private Texture _shkafTexture;
        private Texture _lavochkaTexture;
        private Texture _tvTexture;
        private Texture _kartina1Texture;
        private Texture _kartina2Texture;
        private Texture _kartina3Texture;
        private Texture _korobkaTexture;
       // private Texture _cubikRubikaTexture;
        private MeshObject _sixGrannik;
        private MeshObject _mainStena1;
        private MeshObject _mainStena2;
        private MeshObject _mainStena3;
        private MeshObject _mainStena4;
        private MeshObject _stena1;
        private MeshObject _stena2;
        private MeshObject _stena3;
        private MeshObject _stena4;
        private MeshObject _stena5;
        private MeshObject _stena6;
        private MeshObject _stena7;
        private MeshObject _stena8;
        private MeshObject _stena9;
        private MeshObject _stena10;
        private MeshObject _stena11;
        private MeshObject _komod1;
        private MeshObject _komod2;
        private MeshObject _lavochka1;
        private MeshObject _lavochka2;
        private MeshObject _lavochka3;
        private MeshObject _lavochka4;
        private MeshObject _plot;
        private MeshObject _potolok;
        private MeshObject _letter1;
        private MeshObject _letter2;
        private MeshObject _letter3;
        private MeshObject _letter4;
        private MeshObject _letter5;
        private MeshObject _divan1;
        private MeshObject _divan2;
        private MeshObject _yaschik2_1;
        private MeshObject _yaschik2_2;
        private MeshObject _yaschik2_3;
        private MeshObject _yaschik2_4;
        private MeshObject _yaschik2_5;
        private MeshObject _yaschik2_6;
        private MeshObject _yaschik2_7;
        private MeshObject _yaschik1;
        private MeshObject _yaschik2;
        private MeshObject _yaschik3;
        private MeshObject _yaschik4;
        private MeshObject _yaschik5;
        private MeshObject _yaschik6;
        private MeshObject _yaschik7;
        private MeshObject _yaschik8;
        private MeshObject _yaschik9;
        private MeshObject _verxYaschik1;
        private MeshObject _verxYaschik2;
        private MeshObject _verxYaschik3;
        private MeshObject _verxYaschik4;
        private MeshObject _verxYaschik5;
        private MeshObject _verxYaschik6;
        private MeshObject _kartina1;
        private MeshObject _kartina2;
        private MeshObject _kartina3;
        private MeshObject _carpet;
        private MeshObject _carpet2_1;
        private MeshObject _carpet2_2;
        private MeshObject _carpet2_3;
        private MeshObject _carpet3_1;
        private MeshObject _carpet3_2;
        private MeshObject _carpet3_3;
        private MeshObject _carpet3_4;
        private MeshObject _xolodilnik;
        private MeshObject _knife;
        private MeshObject _plita;
        private MeshObject _rakovina;
        private MeshObject _shkaf1;
        private MeshObject _shkaf2;
        private MeshObject _cylinder;
        private MeshObject _player;
        private MeshObject _tv;
        private MeshObject _korobka1_1;
        private MeshObject _korobka1_2;
        private MeshObject _korobka1_3;
        private MeshObject _korobka1_4;
        private MeshObject _korobka1_5;
        private MeshObject _korobka1_6;
        private MeshObject _korobka1_7;
        private MeshObject _korobka1_8;
        private MeshObject _korobka1_9;
        private MeshObject _korobka1_10;
        private MeshObject _korobka1_11;
        private MeshObject _korobka1_12;
        private MeshObject _korobka1_13;
        private MeshObject _korobka1_14;
       //private MeshObject _cubikRubika;

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

        private MaterialProperties _defaultObjectMaterial;
        private MaterialProperties _changedObjectMaterial;
        private MaterialProperties _currentObjectMaterial;


        private MaterialProperties _rayMaterial;

        private LightProperties _light;

        private bool _isMap = false;

        private BoundingBox _playerCollider;

        private BoundingBox _sixGrannikCollider;

        private BoundingBox _plotCollider;
        private BoundingBox _potolokCollider;

        private BoundingSphere _cylinderCollider;


        private BoundingBox _letter1Collider;
        private BoundingBox _letter2Collider;
        private BoundingBox _letter3Collider;
        private BoundingBox _letter4Collider;
        private BoundingBox _letter5Collider;

        private BoundingBox _mainStenaCollider1;
        private BoundingBox _mainStenaCollider2;
        private BoundingBox _mainStenaCollider3;
        private BoundingBox _mainStenaCollider4;

        private BoundingBox _yaschik2_1Collider;
        private BoundingBox _yaschik2_2Collider;
        private BoundingBox _yaschik2_3Collider;
        private BoundingBox _yaschik2_4Collider;
        private BoundingBox _yaschik2_5Collider;
        private BoundingBox _yaschik2_6Collider;
        private BoundingBox _yaschik2_7Collider;

        private BoundingBox _yaschik1Collider;
        private BoundingBox _yaschik2Collider;
        private BoundingBox _yaschik3Collider;
        private BoundingBox _yaschik4Collider;
        private BoundingBox _yaschik5Collider;
        private BoundingBox _yaschik6Collider;
        private BoundingBox _yaschik7Collider;
        private BoundingBox _yaschik8Collider;
        private BoundingBox _yaschik9Collider;
        private BoundingBox _verxYaschik1Collider;
        private BoundingBox _verxYaschik2Collider;
        private BoundingBox _verxYaschik3Collider;
        private BoundingBox _verxYaschik4Collider;
        private BoundingBox _verxYaschik5Collider;
        private BoundingBox _verxYaschik6Collider;
        private BoundingBox _divan1Collider;
        private BoundingBox _divan2Collider;
        private BoundingBox _xolodilnikCollider;
        private BoundingBox _plitaCollider;
        private BoundingBox _rakovinaCollider;
        private BoundingBox _knifeCollider;
        private BoundingBox _stena1Collider;
        private BoundingBox _stena2Collider;
        private BoundingBox _stena3Collider;
        private BoundingBox _stena4Collider;
        private BoundingBox _stena5Collider;
        private BoundingBox _stena6Collider;
        private BoundingBox _stena7Collider;
        private BoundingBox _stena8Collider;
        private BoundingBox _stena9Collider;
        private BoundingBox _stena10Collider;
        private BoundingBox _stena11Collider;
        private BoundingBox _shkaf1Collider;
        private BoundingBox _shkaf2Collider;
        private BoundingBox _komod1Collider;
        private BoundingBox _komod2Collider;
        private BoundingBox _lavochka1Collider;
        private BoundingBox _lavochka2Collider;
        private BoundingBox _lavochka3Collider;
        private BoundingBox _lavochka4Collider;
        private BoundingBox _korobka1Collider;
        private BoundingBox _korobka2Collider;
        private BoundingBox _korobka3Collider;
        private BoundingBox _korobka4Collider;
        private BoundingBox _korobka5Collider;
        private BoundingBox _korobka6Collider;
        private BoundingBox _korobka7Collider;
        private BoundingBox _korobka8Collider;
        private BoundingBox _korobka9Collider;
        private BoundingBox _korobka10Collider;
        private BoundingBox _korobka11Collider;
        private BoundingBox _korobka12Collider;
        private BoundingBox _korobka13Collider;
        private BoundingBox _korobka14Collider;

        private string textFromLettersField = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
        private TextLayout _textLayout = null;
        private TextFormat _testTextFormat;
        //private BoundingBox _cubikRubikaCollider;

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

        private void DrawHUD()
        {
            _testTextFormat = new TextFormat(_directX3DGraphics.FactoryDWrite, "Calibri", 28)
            {
                TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                ParagraphAlignment = ParagraphAlignment.Center,
            };
            _directX3DGraphics.D2DRenderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;

            _directX3DGraphics.D2DRenderTarget.BeginDraw();

            _textLayout = new TextLayout(_directX3DGraphics.FactoryDWrite, textFromLettersField, _testTextFormat, _renderForm.Width, _renderForm.Height);
            _directX3DGraphics.D2DRenderTarget.DrawTextLayout(new SharpDX.Mathematics.Interop.RawVector2(_renderForm.Width / 2, _renderForm.Height / 2), _textLayout, _whiteBrush, DrawTextOptions.None);
            
            _directX3DGraphics.D2dContext.EndDraw();

            _directX3DGraphics.SwapChain.Present(0, PresentFlags.None);
        }

        private void InvokeInitializers()
        {
            InitializeGame();
            InitializeMaterials();
            CreateLights();

            InitializeTextures(_loader);
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

        private void InitializeTextures(Loader loader)
        {
            _sixGrannikTexture = LoadTexture(loader, "6grannik.png");
            _cylinderTexture = LoadTexture(loader, "CocaCola.png");
            _plotTexture = LoadTexture(loader, "floor.jpg");
            oboiTexture = LoadTexture(loader, "oboiGood.png");
            oboiTexture2 = LoadTexture(loader, "oboiGoodV2.png");
            _stenaTexture = LoadTexture(loader, "stena.jpg");
            _playerTexture = LoadTexture(loader, "CocaCola.png");
            _potolokTexture = LoadTexture(loader, "potolok2.jpg");
            _letterTexture = LoadTexture(loader, "letter.jpg");
            _divan1Texture = LoadTexture(loader, "divan.jpg");
            _divan2Texture = LoadTexture(loader, "divan2.jpg");
            _xolodilnikTexture = LoadTexture(loader, "xolodilnik.png");
            _plitaTexture = LoadTexture(loader, "plita.png");
            _rakovinaTexture = LoadTexture(loader, "rakovina.png");
            _carpetTexture = LoadTexture(loader, "carpet.jpg");
            _carpet2Texture = LoadTexture(loader, "carpet2.jpg");
            _carpet3Texture = LoadTexture(loader, "carpet3.jpg");
            _yaschikTexture = LoadTexture(loader, "yaschik.jpg");
            _verxYaschikTexture = LoadTexture(loader, "verx_yaschik.png");
            _shkafTexture = LoadTexture(loader, "shkaf.jpg");
            _komodTexture = LoadTexture(loader, "yaschik2.png");
            _lavochkaTexture = LoadTexture(loader, "doska2.jpg");
            _tvTexture = LoadTexture(loader, "tv.jpg");
            _kartina1Texture = LoadTexture(loader, "kartina1.png");
            _kartina2Texture = LoadTexture(loader, "kartina2.png");
            _kartina3Texture = LoadTexture(loader, "kartina3.png");
            _korobkaTexture = LoadTexture(loader, "korobka.png");
        }

        private void InitializeGameObjects(Loader loader)
        {
            _plotSize = new Vector2(100.0f, 100.0f);
            _plot = loader.MakePlot(new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 0.0f, 0.0f, _plotSize.X, _plotSize.Y, 0f, ref _plotCollider);

            _player = LoadObjectWithCollider(loader, "player.obj", new Vector4(0f, 0f, 0f, 1f), ref _playerTexture, out _playerCollider);

            _cylinder = LoadObject(loader, "cylinder.obj", new Vector4(-18f, 2.0f, -1.0f, 1.0f), ref _cylinderTexture);
            _sixGrannik = LoadObjectWithCollider(loader, "6grannik.obj", new Vector4(10.0f, 2.0f, 18f, 1.0f), ref _sixGrannikTexture, out _sixGrannikCollider);
            
            _mainStena1 = LoadObjectWithCollider(loader, "oboiWall_perpV2.obj", new Vector4(0f, 0f, 20f, 1.0f), ref oboiTexture, out _mainStenaCollider1);
            _mainStena2 = LoadObjectWithCollider(loader, "oboiWall_perp.obj", new Vector4(0f, 0f, -20f, 1.0f), ref oboiTexture, out _mainStenaCollider2);
            _mainStena3 = LoadObjectWithCollider(loader, "oboiWall.obj", new Vector4(-20f, 0f, 0f, 1.0f), ref oboiTexture, out _mainStenaCollider3);
            _mainStena4 = LoadObjectWithCollider(loader, "oboiWall.obj", new Vector4(20f, 0f, 0f, 1.0f), ref oboiTexture, out _mainStenaCollider4);
            _potolok = LoadObject(loader, "potolok2.obj", new Vector4(0f, 4f, 0f, 1.0f), ref _potolokTexture);
            
            _letter1 = LoadObjectWithCollider(loader, "letter.obj", new Vector4(-8.0f, 2.01f, 14, 1.0f), ref _letterTexture, out _letter1Collider);
            _letter2 = LoadObjectWithCollider(loader, "letter.obj", new Vector4(-18f, 1.63f, -16f, 1.0f), ref _letterTexture, out _letter2Collider);
            _letter3 = LoadObjectWithCollider(loader, "letter.obj", new Vector4(18f, 1.25f, 1f, 1.0f), ref _letterTexture, out _letter3Collider);
            _letter4 = LoadObjectWithCollider(loader, "letter.obj", new Vector4(-2.0f, 1.25f, -12f, 1.0f), ref _letterTexture, out _letter4Collider); // на столике возле дивана
            _letter5 = LoadObjectWithCollider(loader, "letter.obj", new Vector4(-18f, 2f, 7, 1.0f), ref _letterTexture, out _letter5Collider);
            
            _divan1 = LoadObjectWithCollider(loader, "divan.obj", new Vector4(12f, -0.7f, -15f, 1.0f), ref _divan1Texture, out _divan1Collider);
            _divan2 = LoadObjectWithCollider(loader, "divan2.obj", new Vector4(-3f, -0.75f, -17f, 1.0f), ref _divan2Texture, out _divan2Collider);
            
            _carpet = LoadObject(loader, "carpet.obj", new Vector4(12f, 2f, -19f, 1.0f), ref _carpetTexture);
            _carpet2_1 = LoadObject(loader, "carpet2.obj", new Vector4(12f, 0.1f, -13f, 1.0f), ref _carpet2Texture);
            _carpet2_2 = LoadObject(loader, "carpet2.obj", new Vector4(-14f, 0.1f, 16f, 1.0f), ref _carpet2Texture);
            _carpet2_3 = LoadObject(loader, "carpet2.obj", new Vector4(-14f, 0.1f, 9.99f, 1.0f), ref _carpet2Texture);
            _carpet3_1 = LoadObject(loader, "carpet3.obj", new Vector4(-3f, 0.1f, -0.5f, 1.0f), ref _carpet3Texture);
            _carpet3_2 = LoadObject(loader, "carpet3.obj", new Vector4(3f, 0.1f, -0.5f, 1.0f), ref _carpet3Texture);
            _carpet3_3 = LoadObject(loader, "carpet3.obj", new Vector4(9f, 0.1f, -0.5f, 1.0f), ref _carpet3Texture);
            _carpet3_4 = LoadObject(loader, "carpet3.obj", new Vector4(15f, 0.1f, -0.5f, 1.0f), ref _carpet3Texture);
            
            _tv = LoadObject(loader, "tv.obj", new Vector4(-2f, 2f, -6.05f, 1.0f), ref _tvTexture);
            
            _yaschik1 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -18f, 1.0f), ref _yaschikTexture, out _yaschik1Collider);
            _yaschik2 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -16.4f, 1.0f), ref _yaschikTexture, out _yaschik1Collider);
            _yaschik3 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -14.8f, 1.0f), ref _yaschikTexture, out _yaschik2Collider);
            _yaschik4 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, 18f, 1.0f), ref _yaschikTexture, out _yaschik3Collider);
            _yaschik5 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -11.6f, 1.0f), ref _yaschikTexture, out _yaschik4Collider);
            _yaschik6 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -10.0f, 1.0f), ref _yaschikTexture, out _yaschik5Collider);
            _yaschik7 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-14.8f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik7Collider);
            _yaschik8 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-13.2f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik8Collider);
            _yaschik9 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-11.6f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik9Collider);
            
            _verxYaschik1 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -18f, 1.0f), ref _verxYaschikTexture, out _verxYaschik1Collider);
            _verxYaschik2 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -16.4f, 1.0f), ref _verxYaschikTexture, out _verxYaschik2Collider);
            _verxYaschik3 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -14.8f, 1.0f), ref _verxYaschikTexture, out _verxYaschik3Collider);
            _verxYaschik4 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -13.2f, 1.0f), ref _verxYaschikTexture, out _verxYaschik4Collider);
            _verxYaschik5 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -11.6f, 1.0f), ref _verxYaschikTexture, out _verxYaschik5Collider);
            _verxYaschik6 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -10.0f, 1.0f), ref _verxYaschikTexture, out _verxYaschik5Collider);
            
            _xolodilnik = LoadObjectWithCollider(loader, "xolodilnik.obj", new Vector4(-18f, 1.5f, -7.5f, 1.0f), ref _xolodilnikTexture, out _xolodilnikCollider);
            _plita = LoadObjectWithCollider(loader, "plita.obj", new Vector4(-16.4f, 0.8f, -18f, 1.0f), ref _plitaTexture, out _plitaCollider);
            _rakovina = LoadObjectWithCollider(loader, "rakovina.obj", new Vector4(-18f, 0.8f, -13.2f, 1.0f), ref _rakovinaTexture, out _rakovinaCollider);
            
            _stena1 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f, 0f, -14f, 1.0f), ref _stenaTexture, out _stena1Collider);
            _stena2 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f,0f, 10f, 1.0f), ref _stenaTexture, out _stena2Collider);
            _stena3 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(0f,0f, 4f, 1.0f), ref _stenaTexture, out _stena3Collider);
            _stena4 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(-25f,0f, 1f, 1.0f), ref _stenaTexture, out _stena4Collider);
            _stena5 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f,0f, 10f, 1.0f), ref _stenaTexture, out _stena5Collider);
            _stena6 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(4f,0f, -5f, 1.0f), ref _stenaTexture, out _stena6Collider);
            _stena7 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(24f,0f, 4.0001f, 1.0f), ref _stenaTexture, out _stena7Collider);
            _stena8 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(12f,0f, -4.999f, 1.0f), ref _stenaTexture, out _stena8Collider);
            _stena9 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(6f,0f, -19, 1.0f), ref _stenaTexture, out _stena8Collider);
            _stena10 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(15f,0f, 15, 1.0f), ref _stenaTexture, out _stena8Collider);
            _stena11 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(-25f,0f, -5f, 1.0f), ref _stenaTexture, out _stena11Collider);
            
            _shkaf1 = LoadObjectWithCollider(loader, "shkaf.obj", new Vector4(-18f,1.8f, 14, 1.0f), ref _shkafTexture, out _shkaf1Collider);
            _shkaf2 = LoadObjectWithCollider(loader, "shkaf180.obj", new Vector4(18f,1.8f, -13, 1.0f), ref _shkafTexture, out _shkaf2Collider);
            
            _komod1 = LoadObjectWithCollider(loader, "yaschik2.obj", new Vector4(-18f,1f, 7, 1.0f), ref _komodTexture, out _komod1Collider);
            _komod2 = LoadObjectWithCollider(loader, "yaschik2.obj", new Vector4(-8f,1f, 7f, 1.0f), ref _komodTexture, out _komod2Collider);
            
            _lavochka1 = LoadObjectWithCollider(loader, "lavochka.obj", new Vector4(18.0f, 0.6f, 1.25f, 1.0f), ref _lavochkaTexture, out _lavochka1Collider);
            _lavochka2 = LoadObjectWithCollider(loader, "lavochka.obj", new Vector4(18.0f, 0.6f, -2.25f, 1.0f), ref _lavochkaTexture, out _lavochka2Collider);
            _lavochka3 = LoadObjectWithCollider(loader, "lavochka.obj", new Vector4(-1.0f, 0.6f, -12f, 1.0f), ref _lavochkaTexture, out _lavochka3Collider);
            _lavochka4 = LoadObjectWithCollider(loader, "lavochka.obj", new Vector4(-3.0f, 0.6f, -12f, 1.0f), ref _lavochkaTexture, out _lavochka4Collider);

            _korobka1_1 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(-8f, 1f, 14f, 1.0f), ref _korobkaTexture, out _korobka1Collider);
            _korobka1_2 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(4.0f, 1f, 13f, 1.0f), ref _korobkaTexture, out _korobka2Collider);
            _korobka1_3 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(2.0f, 1f, 18f, 1.0f), ref _korobkaTexture, out _korobka3Collider);
            _korobka1_4 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(2.0f, 1f, 13f, 1.0f), ref _korobkaTexture, out _korobka4Collider);
            _korobka1_5 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(4.0f, 1f, 18f, 1.0f), ref _korobkaTexture, out _korobka5Collider);
            _korobka1_6 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(8.0f, 1f, 18f, 1.0f), ref _korobkaTexture, out _korobka6Collider);
            _korobka1_7 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(11.0f, 1f, 10f, 1.0f), ref _korobkaTexture, out _korobka7Collider);
            _korobka1_8 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(13.0f, 1f, 12f, 1.0f), ref _korobkaTexture, out _korobka8Collider);
            _korobka1_9 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(13.0f, 1f, 18f, 1.0f), ref _korobkaTexture, out _korobka9Collider);
            _korobka1_10 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(2.0f, 1f, 6f, 1.0f), ref _korobkaTexture, out _korobka10Collider);
            _korobka1_11 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(2.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka10Collider);
            _korobka1_12 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(4.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka10Collider);
            _korobka1_13 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(6.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka10Collider);
            _korobka1_14 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(13.0f, 1f, 10f, 1.0f), ref _korobkaTexture, out _korobka10Collider);

            _yaschik2_1 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 10f, 1.0f), ref _yaschikTexture, out _yaschik2_1Collider);
            _yaschik2_2 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 12f, 1.0f), ref _yaschikTexture, out _yaschik2_2Collider);
            _yaschik2_3 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 16f, 1.0f), ref _yaschikTexture, out _yaschik2_3Collider);
            _yaschik2_4 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(4.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_4Collider);
            _yaschik2_5 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(6.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_5Collider);
            _yaschik2_6 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(0.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_6Collider);
            _yaschik2_7 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-4.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_7Collider);
            
            _knife = LoadObjectWithCollider(loader, "knife1.obj", new Vector4(2f, 1.2f, 0f, 1.0f), ref _knifeTexture, out _knifeCollider);

            _kartina1 = LoadObject(loader, "kartina1.obj", new Vector4(19f, 3f, -0.5f, 1.0f), ref _kartina1Texture);
            _kartina2 = LoadObject(loader, "kartina2.obj", new Vector4(5f, 2.5f, -13.05f, 1.0f), ref _kartina2Texture);
            _kartina3 = LoadObject(loader, "kartina3.obj", new Vector4(14f, 2.5f, 15f, 1.0f), ref _kartina3Texture);

            _camera = new Camera(new Vector4(_player.Position.X, 2.5f, _player.Position.Z, 1.0f));
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
            _colliders.Add(loader.MakeBoxCollider(_playerCollider, new Vector4(0f, 0f, 0f, 1.0f), 0f, 0f, 0.0f));
            _cylinderCollider = new BoundingSphere(new Vector3(_cylinder.Position.X, _cylinder.Position.Y, _cylinder.Position.Z), 1.0f);
            _colliders.Add(loader.MakeBoxCollider(_sixGrannikCollider, new Vector4(_sixGrannik.Position.X, _sixGrannik.Position.Y, _sixGrannik.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_plotCollider, new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeSphereCollider(_cylinderCollider, 0.0f, 0.0f, 0.0f));

            _colliders.Add(loader.MakeBoxCollider(_mainStenaCollider1, new Vector4(_mainStena1.Position.X, _mainStena1.Position.Y, _mainStena1.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_mainStenaCollider2, new Vector4(_mainStena2.Position.X, _mainStena2.Position.Y, _mainStena2.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_mainStenaCollider3, new Vector4(_mainStena3.Position.X, _mainStena3.Position.Y, _mainStena3.Position.Z, 1.0f), 0f, 0f, 0.0f));
            
            _colliders.Add(loader.MakeBoxCollider(_divan1Collider, new Vector4(_divan1.Position.X, _divan1.Position.Y, _divan1.Position.Z, 1.0f), 0f, 0f, 0.0f));
            _colliders.Add(loader.MakeBoxCollider(_xolodilnikCollider, new Vector4(_xolodilnik.Position.X, _xolodilnik.Position.Y, _xolodilnik.Position.Z, 1.0f), 0f, 0f, 0.0f));
            
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

            _dxInput.Update();


          

            _camera.YawBy(_dxInput.GetMouseDeltaX() * 0.001f);
            _camera.PitchBy(_dxInput.GetMouseDeltaY() * 0.001f);

            _cameraRay.Direction = _camera.GetViewTo();

            CheckPlayerIntersects();
            CheckPlayerInteracts();

            RenderObjects();

            //DrawHUD();

            _renderer.EndRender();
        }
        private void CheckPlayerIntersects()
        {
            Vector3 playerMovement = InputMovement();
            // Попытка перемещения
            Vector3 attemptedMovement = playerMovement;
            _playerCollider.Minimum += attemptedMovement;
            _playerCollider.Maximum += attemptedMovement;

            // Проверка на столкновение
            if (_sixGrannikCollider.Intersects(ref _playerCollider) ||
                _cylinderCollider.Intersects(ref _playerCollider) ||
                _mainStenaCollider1.Intersects(ref _playerCollider) ||
                _mainStenaCollider2.Intersects(ref _playerCollider) ||
                _mainStenaCollider3.Intersects(ref _playerCollider) ||
                _mainStenaCollider4.Intersects(ref _playerCollider) ||
                _yaschik1Collider.Intersects(ref _playerCollider) ||
                _yaschik2Collider.Intersects(ref _playerCollider) ||
                _yaschik3Collider.Intersects(ref _playerCollider) ||
                _yaschik4Collider.Intersects(ref _playerCollider) ||
                _yaschik5Collider.Intersects(ref _playerCollider) ||
                _verxYaschik1Collider.Intersects(ref _playerCollider) ||
                _verxYaschik2Collider.Intersects(ref _playerCollider) ||
                _verxYaschik3Collider.Intersects(ref _playerCollider) ||
                _verxYaschik4Collider.Intersects(ref _playerCollider) ||
                _verxYaschik5Collider.Intersects(ref _playerCollider) ||
                _plitaCollider.Intersects(ref _playerCollider) ||
                _xolodilnikCollider.Intersects(ref _playerCollider) ||
                _knifeCollider.Intersects(ref _playerCollider)
                )
            {
                // Если произошло столкновение, отменяем перемещение
                _playerCollider.Minimum -= attemptedMovement;
                _playerCollider.Maximum -= attemptedMovement;
            }
            else
            {
                // Если столкновения нет, применяем перемещение
                _camera.MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);
                _colliders[0].MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);
            }

            _cameraRay.Position += playerMovement;

            if (_dxInput.IsKeyPressed(Key.U))
            {
                _cylinder.MoveBy(0f, 0.5f, 0f);
                _colliders[2].MoveBy(0f, 0.5f, 0f);
                _cylinderCollider.Center += new Vector3(0f, 0.5f, 0f);
            }
        }

        private void CheckPlayerInteracts()
        {
            if (_cameraRay.Intersects(ref _lavochka3Collider))
            {
                Debug.WriteLine("Interact with lavochka3");
            }
         
            if (_cameraRay.Intersects(ref _letter1Collider) && _dxInput.IsKeyPressed(Key.E))
            {
                //TODO: сделать текст письма
                Debug.WriteLine("PISMO 1!!! ");
            }
            if (_cameraRay.Intersects(ref _letter2Collider) && _dxInput.IsKeyPressed(Key.E))
            {
                //TODO: сделать текст письма
                Debug.WriteLine("PISMO 2!!! ");
            }
            if (_cameraRay.Intersects(ref _letter3Collider) && _dxInput.IsKeyPressed(Key.E))
            {
                //TODO: сделать текст письма
                Debug.WriteLine("PISMO 3!!! ");
            }
            if (_cameraRay.Intersects(ref _letter4Collider) && _dxInput.IsKeyPressed(Key.E))
            {
                //TODO: сделать текст письма

                Debug.WriteLine("PISMO 4!!! ");
            }
            if (_cameraRay.Intersects(ref _letter5Collider) && _dxInput.IsKeyPressed(Key.E))
                {
                //TODO: сделать текст письма
                    Debug.WriteLine("PISMO 5!!! ");
                }
            /*if(_lavochka3Collider.Intersects(ref _playerCollider))
            {
                Debug.WriteLine("Игрок взаимодействует с объектом lavochka3Collider");
            }*/
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
                    Ambient = new Vector4(0.0f, 0.2f, 0.0f, 1f), // Зеленый оттенок
                    Diffuse = new Vector4(0.0f, 0.5f, 0.0f, 1.0f), // Зеленый оттенок
                    Specular = new Vector4(0.0f, 0.7f, 0.0f, 1f), // Зеленый оттенок
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

            _currentObjectMaterial = _floorMaterial;

            _currentObjectMaterial = _floorMaterial;
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
                _renderer.SetPerObjectConstantBuffer(_defaultObjectMaterial);
                _renderer.UpdatePerObjectConstantBuffers(_colliders[i].GetWorldMatrix(), viewMatrix, projectionMatrix);
                _renderer.RenderMeshObject(_colliders[i]);
            }

            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Solid);

            RenderObject(viewMatrix, projectionMatrix, _sixGrannik, _sixGrannikTexture);
            RenderObject(viewMatrix, projectionMatrix, _cylinder, _cylinderTexture);
            RenderObject(viewMatrix, projectionMatrix, _plot, _plotTexture);
            RenderObject(viewMatrix, projectionMatrix, _mainStena1, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _mainStena2, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _mainStena3, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _mainStena4, oboiTexture);
            RenderObject(viewMatrix, projectionMatrix, _potolok, _potolokTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter1, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter2, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter3, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter4, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _letter5, _letterTexture);
            RenderObject(viewMatrix, projectionMatrix, _divan1, _divan1Texture);
            RenderObject(viewMatrix, projectionMatrix, _divan2, _divan2Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet, _carpetTexture);
            RenderObject(viewMatrix, projectionMatrix, _carpet2_1, _carpet2Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet2_2, _carpet2Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet2_3, _carpet2Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet3_1, _carpet3Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet3_2, _carpet3Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet3_3, _carpet3Texture);
            RenderObject(viewMatrix, projectionMatrix, _carpet3_4, _carpet3Texture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik1, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik3, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik4, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik5, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik6, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik7, _yaschikPerpTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik8, _yaschikPerpTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik9, _yaschikPerpTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik1, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik2, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik3, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik4, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik5, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _verxYaschik6, _verxYaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _xolodilnik, _xolodilnikTexture);
            RenderObject(viewMatrix, projectionMatrix, _plita, _plitaTexture);
            RenderObject(viewMatrix, projectionMatrix, _kartina1, _kartina1Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina2, _kartina2Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina3, _kartina3Texture);
            RenderObject(viewMatrix, projectionMatrix, _rakovina, _rakovinaTexture);
            RenderObject(viewMatrix, projectionMatrix, _knife, _knifeTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena1, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena2, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena3, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena4, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena5, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena6, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena7, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena8, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena9, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena10, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _stena11, _stenaTexture);
            RenderObject(viewMatrix, projectionMatrix, _shkaf1, _shkafTexture);
            RenderObject(viewMatrix, projectionMatrix, _shkaf2, _shkafTexture);
            RenderObject(viewMatrix, projectionMatrix, _komod1, _komodTexture);
            RenderObject(viewMatrix, projectionMatrix, _komod2, _komodTexture);
            RenderObject(viewMatrix, projectionMatrix, _lavochka1, _lavochkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _lavochka2, _lavochkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _lavochka3, _lavochkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _lavochka4, _lavochkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _tv, _tvTexture);

            RenderObject(viewMatrix, projectionMatrix, _korobka1_1, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_2, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_3, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_4, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_5, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_6, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_7, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_8, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_9, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_10, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_11, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_12, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_13, _korobkaTexture);
            RenderObject(viewMatrix, projectionMatrix, _korobka1_14, _korobkaTexture);

            RenderObject(viewMatrix, projectionMatrix, _yaschik2_1, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_2, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_3, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_4, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_5, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_6, _yaschikTexture);
            RenderObject(viewMatrix, projectionMatrix, _yaschik2_7, _yaschikTexture);

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