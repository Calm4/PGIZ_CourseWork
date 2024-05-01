using Lab01.App.Scripts.DirectX;
using Lab01.App.Scripts.Environment;
using Lab01.App.Scripts.Textures;
using ObjLoader.Loader.Loaders;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab01.Scripts.Game
{
    public class GameRender
    {
        private Texture _kartina1Texture;
        private Texture _kartina2Texture;
        private Texture _kartina3Texture;
        private Texture _kartina4Texture;
        private Texture _kartina5Texture;
        private Texture _kartina6Texture;
        private Texture _kartina7Texture;
        private Texture _kartina8Texture;
        private Texture _kartina9Texture;
        private Texture _kartina10Texture;

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
        private Texture _korobkaTexture;

        //---------------------------------------------------------------------------------------------------------------------------------------
        private MeshObject _kartina1;
        private MeshObject _kartina2;
        private MeshObject _kartina3;
        private MeshObject _kartina4;
        private MeshObject _kartina5;
        private MeshObject _kartina6;
        private MeshObject _kartina7;
        private MeshObject _kartina8;
        private MeshObject _kartina9;
        private MeshObject _kartina10;
        //---------------------------------------------------------------------------------------------------------------------------------------

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

        private MeshObject _potolok;
        private MeshObject _plot;

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
        private MeshObject _komod1;
        private MeshObject _komod2;
        private MeshObject _lavochka1;
        private MeshObject _lavochka2;
        private MeshObject _lavochka3;
        private MeshObject _lavochka4;

        private BoundingBox _playerCollider;

        private BoundingBox _sixGrannikCollider;

        private BoundingBox _plotCollider;
        private BoundingBox _potolokCollider;

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

        private BoundingBox _kartina1Collider;
        private BoundingBox _kartina2Collider;
        private BoundingBox _kartina3Collider;
        private BoundingBox _kartina4Collider;
        private BoundingBox _kartina5Collider;
        private BoundingBox _kartina6Collider;
        private BoundingBox _kartina7Collider;
        private BoundingBox _kartina8Collider;
        private BoundingBox _kartina9Collider;
        private BoundingBox _kartina10Collider;

        private Renderer _renderer;
        private DirectX3DGraphics _directX3DGraphics;
        private List<MeshObject> _meshObjects;
        private Loader _loader;
        private Camera _camera;
        TimeHelper _timeHelper;

        Vector2 _plotSize;
        Vector2 _potolokSize;

        private TextLayout _helpTextLayout = null;
        private TextLayout _kartina1TextField = null;
        private TextLayout _kartina2TextField = null;
        private TextLayout _kartina3TextField = null;
        private TextLayout _kartina4TextField = null;
        private TextLayout _kartina5TextField = null;
        private TextLayout _kartina6TextField = null;
        private TextLayout _kartina7TextField = null;
        private TextLayout _kartina8TextField = null;
        private TextLayout _kartina9TextField = null;
        private TextLayout _kartina10TextField = null;

        private string textFromLettersField = "Добро пожаловать!";

        private string pictureNameField1 = "Бурлаки на Волге";
        private string pictureNameField2 = "Утро в сосновом лесу";
        private string pictureNameField3 = "Пуститься в плаванье";
        private string pictureNameField4 = "Мона Лиза";
        private string pictureNameField5 = "Крик";
        private string pictureNameField6 = "Ночной дозор";
        private string pictureNameField7 = "Богатыри";
        private string pictureNameField8 = "Грачи прилетели";
        private string pictureNameField9 = "Тайная вечеря";
        private string pictureNameField10 = "Девятый вал";

        private Color _currentColorPicture1 = Color.White;
        private Color _currentColorPicture2 = Color.White;
        private Color _currentColorPicture3 = Color.White;
        private Color _currentColorPicture4 = Color.White;
        private Color _currentColorPicture5 = Color.White;
        private Color _currentColorPicture6 = Color.White;
        private Color _currentColorPicture7 = Color.White;
        private Color _currentColorPicture8 = Color.White;
        private Color _currentColorPicture9 = Color.White;
        private Color _currentColorPicture10 = Color.White;

        private TextFormat _testTextFormat;
        private TextFormat _picturesTextFormat;

        private const string GameObjectsPath = "GameObjects/";
        private const string ImagesPath = GameObjectsPath + "Images/";
        private const string ObjectsPath = GameObjectsPath + "Objects/";

        private float _textDisplayTime = 0f;
        private const float TextDisplayDuration = 3f;

        private Ray _cameraRay;
        List<BoundingBox> _colliders = new List<BoundingBox>();


        public GameRender(Renderer renderer, DirectX3DGraphics directX3DGraphics, List<MeshObject> meshObjects, Loader loader, Camera camera, TimeHelper timeHelper)
        {
            _renderer = renderer;
            _directX3DGraphics = directX3DGraphics;
            _meshObjects = meshObjects;
            _loader = loader;
            _camera = camera;
            _timeHelper = timeHelper;



            InitializeGameObjects(_loader);
            InitializeColliders();
            InitializeTextures(_loader);
        }

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
            _korobkaTexture = LoadTexture(loader, "korobka.png");


            _kartina1Texture = LoadTexture(loader, "kartina1_burlaki.png");
            _kartina2Texture = LoadTexture(loader, "kartina2_ytro.png");
            _kartina3Texture = LoadTexture(loader, "kartina3_plavanie.png");
            _kartina4Texture = LoadTexture(loader, "kartina4_monaliza.png");
            _kartina5Texture = LoadTexture(loader, "kartina5_krik.png");
            _kartina6Texture = LoadTexture(loader, "kartina6_dozor.png");
            _kartina7Texture = LoadTexture(loader, "kartina7_bogatblri.png");
            _kartina8Texture = LoadTexture(loader, "kartina8_grachiprileteli.png");
            _kartina9Texture = LoadTexture(loader, "kartina9_taynayavechere.png");
            _kartina10Texture = LoadTexture(loader, "kartina10_9val.png");
        }
        private Texture LoadTexture(Loader loader, string fileName)
        {
            return loader.LoadTextureFromFile(ImagesPath + fileName, _renderer.AnisotropicSampler);
        }
        private void InitializeColliders()
        {
            _colliders.Add(_sixGrannikCollider);
            //_colliders.Add(_plotCollider);
            //_colliders.Add(_potolokCollider);

            _colliders.Add(_letter1Collider);
            _colliders.Add(_letter2Collider);
            _colliders.Add(_letter3Collider);
            _colliders.Add(_letter4Collider);

            _colliders.Add(_mainStenaCollider1);
            _colliders.Add(_mainStenaCollider2);
            _colliders.Add(_mainStenaCollider3);
            _colliders.Add(_mainStenaCollider4);

            _colliders.Add(_yaschik2_1Collider);
            _colliders.Add(_yaschik2_2Collider);
            _colliders.Add(_yaschik2_3Collider);
            _colliders.Add(_yaschik2_4Collider);
            _colliders.Add(_yaschik2_5Collider);
            _colliders.Add(_yaschik2_6Collider);
            _colliders.Add(_yaschik2_7Collider);

            _colliders.Add(_yaschik1Collider);
            _colliders.Add(_yaschik2Collider);
            _colliders.Add(_yaschik3Collider);
            _colliders.Add(_yaschik4Collider);
            _colliders.Add(_yaschik5Collider);
            _colliders.Add(_yaschik6Collider);
            _colliders.Add(_yaschik7Collider);
            _colliders.Add(_yaschik8Collider);
            _colliders.Add(_yaschik9Collider);

            _colliders.Add(_verxYaschik1Collider);
            _colliders.Add(_verxYaschik2Collider);
            _colliders.Add(_verxYaschik3Collider);
            _colliders.Add(_verxYaschik4Collider);
            _colliders.Add(_verxYaschik5Collider);
            _colliders.Add(_verxYaschik6Collider);

            _colliders.Add(_divan1Collider);
            _colliders.Add(_divan2Collider);

            _colliders.Add(_stena1Collider);
            _colliders.Add(_stena2Collider);
            _colliders.Add(_stena3Collider);
            _colliders.Add(_stena4Collider);
            _colliders.Add(_stena5Collider);
            _colliders.Add(_stena6Collider);
            _colliders.Add(_stena7Collider);
            _colliders.Add(_stena8Collider);
            _colliders.Add(_stena9Collider);
            _colliders.Add(_stena10Collider);
            _colliders.Add(_stena11Collider);

            _colliders.Add(_xolodilnikCollider);
            _colliders.Add(_plitaCollider);
            _colliders.Add(_rakovinaCollider);

            _colliders.Add(_shkaf1Collider);
            _colliders.Add(_shkaf2Collider);

            _colliders.Add(_komod1Collider);
            _colliders.Add(_komod2Collider);

            _colliders.Add(_lavochka1Collider);
            _colliders.Add(_lavochka2Collider);
            _colliders.Add(_lavochka3Collider);
            _colliders.Add(_lavochka4Collider);

            _colliders.Add(_korobka1Collider);
            _colliders.Add(_korobka2Collider);
            _colliders.Add(_korobka3Collider);
            _colliders.Add(_korobka4Collider);
            _colliders.Add(_korobka5Collider);
            _colliders.Add(_korobka6Collider);
            _colliders.Add(_korobka7Collider);
            _colliders.Add(_korobka8Collider);
            _colliders.Add(_korobka9Collider);
            _colliders.Add(_korobka10Collider);
            _colliders.Add(_korobka11Collider);
            _colliders.Add(_korobka12Collider);
            _colliders.Add(_korobka13Collider);
            _colliders.Add(_korobka14Collider);

            _colliders.Add(_kartina1Collider);
            _colliders.Add(_kartina2Collider);
            _colliders.Add(_kartina3Collider);
            _colliders.Add(_kartina4Collider);
            _colliders.Add(_kartina5Collider);
            _colliders.Add(_kartina6Collider);
            _colliders.Add(_kartina7Collider);
            _colliders.Add(_kartina8Collider);
            _colliders.Add(_kartina9Collider);
            _colliders.Add(_kartina10Collider);
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
            _yaschik2 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -16.4f, 1.0f), ref _yaschikTexture, out _yaschik2Collider);
            _yaschik3 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -14.8f, 1.0f), ref _yaschikTexture, out _yaschik3Collider);
            _yaschik4 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, 18f, 1.0f), ref _yaschikTexture, out _yaschik4Collider);
            _yaschik5 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -11.6f, 1.0f), ref _yaschikTexture, out _yaschik5Collider);
            _yaschik6 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-18f, 0.8f, -10.0f, 1.0f), ref _yaschikTexture, out _yaschik6Collider);
            _yaschik7 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-14.8f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik7Collider);
            _yaschik8 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-13.2f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik8Collider);
            _yaschik9 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-11.6f, 0.8f, -18f, 1.0f), ref _yaschikPerpTexture, out _yaschik9Collider);

            _verxYaschik1 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -18f, 1.0f), ref _verxYaschikTexture, out _verxYaschik1Collider);
            _verxYaschik2 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -16.4f, 1.0f), ref _verxYaschikTexture, out _verxYaschik2Collider);
            _verxYaschik3 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -14.8f, 1.0f), ref _verxYaschikTexture, out _verxYaschik3Collider);
            _verxYaschik4 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -13.2f, 1.0f), ref _verxYaschikTexture, out _verxYaschik4Collider);
            _verxYaschik5 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -11.6f, 1.0f), ref _verxYaschikTexture, out _verxYaschik5Collider);
            _verxYaschik6 = LoadObjectWithCollider(loader, "verx_yaschik.obj", new Vector4(-18f, 3.2f, -10.0f, 1.0f), ref _verxYaschikTexture, out _verxYaschik6Collider);

            _xolodilnik = LoadObjectWithCollider(loader, "xolodilnik.obj", new Vector4(-18f, 1.5f, -7.5f, 1.0f), ref _xolodilnikTexture, out _xolodilnikCollider);
            _plita = LoadObjectWithCollider(loader, "plita.obj", new Vector4(-16.4f, 0.8f, -18f, 1.0f), ref _plitaTexture, out _plitaCollider);
            _rakovina = LoadObjectWithCollider(loader, "rakovina.obj", new Vector4(-18f, 0.8f, -13.2f, 1.0f), ref _rakovinaTexture, out _rakovinaCollider);

            _stena1 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f, 0f, -14f, 1.0f), ref _stenaTexture, out _stena1Collider);
            _stena2 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f, 0f, 10f, 1.0f), ref _stenaTexture, out _stena2Collider);
            _stena3 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(0f, 0f, 4f, 1.0f), ref _stenaTexture, out _stena3Collider);
            _stena4 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(-25f, 0f, 1f, 1.0f), ref _stenaTexture, out _stena4Collider);
            _stena5 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(-10f, 0f, 10f, 1.0f), ref _stenaTexture, out _stena5Collider);
            _stena6 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(4f, 0f, -5f, 1.0f), ref _stenaTexture, out _stena6Collider);
            _stena7 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(24f, 0f, 4.0001f, 1.0f), ref _stenaTexture, out _stena7Collider);
            _stena8 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(12f, 0f, -4.999f, 1.0f), ref _stenaTexture, out _stena8Collider);
            _stena9 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(6f, 0f, -19, 1.0f), ref _stenaTexture, out _stena9Collider);
            _stena10 = LoadObjectWithCollider(loader, "stena.obj", new Vector4(15f, 0f, 15, 1.0f), ref _stenaTexture, out _stena10Collider);
            _stena11 = LoadObjectWithCollider(loader, "stena_perp.obj", new Vector4(-25f, 0f, -5f, 1.0f), ref _stenaTexture, out _stena11Collider);

            _shkaf1 = LoadObjectWithCollider(loader, "shkaf.obj", new Vector4(-18f, 1.8f, 14, 1.0f), ref _shkafTexture, out _shkaf1Collider);
            _shkaf2 = LoadObjectWithCollider(loader, "shkaf180.obj", new Vector4(18f, 1.8f, -13, 1.0f), ref _shkafTexture, out _shkaf2Collider);

            _komod1 = LoadObjectWithCollider(loader, "yaschik2.obj", new Vector4(-18f, 1f, 7, 1.0f), ref _komodTexture, out _komod1Collider);
            _komod2 = LoadObjectWithCollider(loader, "yaschik2.obj", new Vector4(-8f, 1f, 7f, 1.0f), ref _komodTexture, out _komod2Collider);

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
            _korobka1_11 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(2.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka11Collider);
            _korobka1_12 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(4.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka12Collider);
            _korobka1_13 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(6.0f, 1f, 11f, 1.0f), ref _korobkaTexture, out _korobka13Collider);
            _korobka1_14 = LoadObjectWithCollider(loader, "korobka.obj", new Vector4(13.0f, 1f, 10f, 1.0f), ref _korobkaTexture, out _korobka14Collider);

            _yaschik2_1 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 10f, 1.0f), ref _yaschikTexture, out _yaschik2_1Collider);
            _yaschik2_2 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 12f, 1.0f), ref _yaschikTexture, out _yaschik2_2Collider);
            _yaschik2_3 = LoadObjectWithCollider(loader, "yaschik.obj", new Vector4(-8.0f, 0.8f, 16f, 1.0f), ref _yaschikTexture, out _yaschik2_3Collider);
            _yaschik2_4 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(4.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_4Collider);
            _yaschik2_5 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(6.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_5Collider);
            _yaschik2_6 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(0.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_6Collider);
            _yaschik2_7 = LoadObjectWithCollider(loader, "yaschik_perp.obj", new Vector4(-4.0f, 0.8f, 6f, 1.0f), ref _yaschikPerpTexture, out _yaschik2_7Collider);

            _kartina1 = LoadObjectWithCollider(loader, "kartina1.obj", new Vector4(19f, 3f, -0.5f, 1.0f), ref _kartina1Texture, out _kartina1Collider);
            _kartina2 = LoadObjectWithCollider(loader, "kartina2.obj", new Vector4(5f, 2.5f, -13.05f, 1.0f), ref _kartina2Texture, out _kartina2Collider);
            _kartina3 = LoadObjectWithCollider(loader, "kartina3.obj", new Vector4(14f, 1f, 15f, 1.0f), ref _kartina3Texture, out _kartina3Collider);
            _kartina4 = LoadObjectWithCollider(loader, "kartina4.obj", new Vector4(-16f, 2.5f, -6f, 1.0f), ref _kartina4Texture, out _kartina4Collider);
            _kartina5 = LoadObjectWithCollider(loader, "kartina5.obj", new Vector4(12f, 2f, -6f, 1.0f), ref _kartina5Texture, out _kartina5Collider);
            _kartina6 = LoadObjectWithCollider(loader, "kartina6.obj", new Vector4(-6f, 1f, 5f, 1.0f), ref _kartina6Texture, out _kartina6Collider);
            _kartina7 = LoadObjectWithCollider(loader, "kartina7.obj", new Vector4(-19f, 2.8f, 3.5f, 1.0f), ref _kartina7Texture, out _kartina7Collider);
            _kartina8 = LoadObjectWithCollider(loader, "kartina8.obj", new Vector4(-9f, 2.5f, 1.5f, 1.0f), ref _kartina8Texture, out _kartina8Collider);
            _kartina9 = LoadObjectWithCollider(loader, "kartina9.obj", new Vector4(-9f, 2.5f, -12f, 1.0f), ref _kartina9Texture, out _kartina9Collider);
            _kartina10 = LoadObjectWithCollider(loader, "kartina10.obj", new Vector4(-14f, 2.5f, 19f, 1.0f), ref _kartina10Texture, out _kartina10Collider);

            _camera = new Camera(new Vector4(_player.Position.X, 2.5f, _player.Position.Z, 1.0f));
            _timeHelper = new TimeHelper();
            _cameraRay = new Ray(new Vector3(_camera.Position.X, _camera.Position.Y, _camera.Position.Z), _camera.GetViewTo());
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

        private void RenderObject(Matrix viewMatrix, Matrix projectionMatrix, MeshObject meshObject, Texture objectTexture)
        {
            _renderer.SetPerObjectConstantBuffer(GameMaterials.CurrentTetrahedronMaterial);
            _renderer.UpdatePerObjectConstantBuffers(meshObject.GetWorldMatrix(), viewMatrix, projectionMatrix);
            _renderer.SetTexture(objectTexture);
            _renderer.RenderMeshObject(meshObject);
        }

        public void CheckPlayerIntersects(DXInput dxInput)
        {
            Vector3 playerMovement = InputMovement(dxInput);

            Vector3 attemptedMovement = playerMovement;
            _playerCollider.Minimum += attemptedMovement;
            _playerCollider.Maximum += attemptedMovement;

            for (int i = 0; i < _colliders.Count; i++)
            {
                BoundingBox collider = _colliders[i];
                if (collider.Intersects(ref _playerCollider))
                {
                    _playerCollider.Minimum -= attemptedMovement;
                    _playerCollider.Maximum -= attemptedMovement;
                    Debug.WriteLine(i);
                    return;
                }
            }

            _camera.MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);
            _player.MoveBy(attemptedMovement.X, attemptedMovement.Y, attemptedMovement.Z);

            _cameraRay.Position += playerMovement;
        }
        public void CheckPlayerInteracts(DXInput dxInput)
        {
           /* if (_cameraRay.Intersects(ref _lavochka3Collider))
            {
                textFromLettersField = "Interact with lavochka3";
                Debug.WriteLine("Interact with lavochka3");
            }*/


            if (_cameraRay.Intersects(ref _letter1Collider) && dxInput.IsKeyPressed(Key.E))
            {
                Debug.WriteLine("PISMO 1!!! ");
                textFromLettersField = "В гардеробной можно найти трех накаченный мужичков!";
                _textDisplayTime = 0f; 
            }
            if (_cameraRay.Intersects(ref _letter2Collider) && dxInput.IsKeyPressed(Key.E))
            {
                Debug.WriteLine("PISMO 2!!! ");
                textFromLettersField = "В комнате которую еще не обустроили должны были остаться картины!";
                _textDisplayTime = 0f; 
            }
            if (_cameraRay.Intersects(ref _letter3Collider) && dxInput.IsKeyPressed(Key.E))
            {
                Debug.WriteLine("PISMO 3!!! ");
                textFromLettersField = "В зале были пустые стены, поэтому их решили украсить картинами!";
                _textDisplayTime = 0f; 
            }
            if (_cameraRay.Intersects(ref _letter4Collider) && dxInput.IsKeyPressed(Key.E))
            {
                Debug.WriteLine("PISMO 4!!! ");
                textFromLettersField = "С дивана хорошо долнжо быть видно картину на которой чуствуется страх!";
                _textDisplayTime = 0f; 
            }
            if (_cameraRay.Intersects(ref _letter5Collider) && dxInput.IsKeyPressed(Key.E))
            {
                Debug.WriteLine("PISMO 5!!! ");
                textFromLettersField = "На кухне возле холодильника спряталась сладкоежка!";
                _textDisplayTime = 0f; 
            }
            if (_currentColorPicture1 == Color.Green && _currentColorPicture2 == Color.Green && _currentColorPicture3 == Color.Green && _currentColorPicture4 == Color.Green && _currentColorPicture5 == Color.Green &&
                _currentColorPicture6 == Color.Green && _currentColorPicture7 == Color.Green && _currentColorPicture8 == Color.Green && _currentColorPicture9 == Color.Green && _currentColorPicture10 == Color.Green)
            {
                textFromLettersField = "Ура вы нашли все картины!";
            }

            if (_cameraRay.Intersects(ref _kartina1Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture1 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina2Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture2 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina3Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture3 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina4Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture4 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina5Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture5 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina6Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture6 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina7Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture7 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina8Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture8 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina9Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture9 = Color.Green;
            }
            if (_cameraRay.Intersects(ref _kartina10Collider) && dxInput.IsKeyPressed(Key.E))
            {
                _currentColorPicture10 = Color.Green;
            }
            /*if(_lavochka3Collider.Intersects(ref _playerCollider))
            {
                Debug.WriteLine("Игрок взаимодействует с объектом lavochka3Collider");
            }*/
            
        }

        private Vector3 InputMovement(DXInput dxInput)
        {
            Vector3 playerMovement = Vector3.Zero;

            if (dxInput.IsKeyPressed(Key.W))
            {
                playerMovement += _camera.GetViewForward() * 0.1f;
            }

            if (dxInput.IsKeyPressed(Key.S))
            {
                playerMovement -= _camera.GetViewForward() * 0.1f;
            }

            if (dxInput.IsKeyPressed(Key.A))
            {
                playerMovement -= _camera.GetViewRight() * 0.1f;
            }

            if (dxInput.IsKeyPressed(Key.D))
            {
                playerMovement += _camera.GetViewRight() * 0.1f;
            }

            return playerMovement;
        }

        public void RenderObjects(Camera _camera, DirectX3DGraphics _directX3DGraphics)
        {
            Matrix viewMatrix = _camera.GetViewMatrix();
            Matrix projectionMatrix = _camera.GetProjectionMatrix();
            GameLights.Light.EyePosition = _camera.Position;
            _renderer.BeginRender();

            _renderer.SetLightConstantBuffer(GameLights.Light);


            _directX3DGraphics.ChangeDisplayType(SharpDX.Direct3D11.FillMode.Wireframe);

            for (int i = 0; i < _meshObjects.Count; i++)
            {
                _renderer.SetPerObjectConstantBuffer(GameMaterials.DefaultObjectMaterial);
                _renderer.UpdatePerObjectConstantBuffers(_meshObjects[i].GetWorldMatrix(), viewMatrix, projectionMatrix);
                _renderer.RenderMeshObject(_meshObjects[i]);
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
            RenderObject(viewMatrix, projectionMatrix, _rakovina, _rakovinaTexture);

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

            RenderObject(viewMatrix, projectionMatrix, _kartina1, _kartina1Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina2, _kartina2Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina3, _kartina3Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina4, _kartina4Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina5, _kartina5Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina6, _kartina6Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina7, _kartina7Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina8, _kartina8Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina9, _kartina9Texture);
            RenderObject(viewMatrix, projectionMatrix, _kartina10, _kartina10Texture);
        }
    }
}
