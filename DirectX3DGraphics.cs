using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Device11 = SharpDX.Direct3D11.Device;

namespace Lab02
{
    class DirectX3DGraphics : IDisposable
    {
        private RenderForm _renderForm;
        public RenderForm RenderForm { get => _renderForm; }

        private SampleDescription _sampleDescription;
        public SampleDescription SampleDescription { get => _sampleDescription; }

        private SwapChainDescription _swapChainDescription;

        private Device11 _device;
        public Device11 Device { get => _device; }

        private SharpDX.DXGI.Device _dxgiDevice;
        public SharpDX.DXGI.Device DxgiDevice { get => _dxgiDevice; }

        private SharpDX.Direct2D1.Device _d2dDevice;
        public SharpDX.Direct2D1.Device D2dDevice { get => _d2dDevice; }

        private SwapChain _swapChain;
        public SwapChain SwapChain { get => _swapChain; }

        private SharpDX.Direct3D11.DeviceContext _deviceContext;
        public SharpDX.Direct3D11.DeviceContext DeviceContext { get => _deviceContext; }

        private SharpDX.Direct2D1.DeviceContext _d2dContext;
        public SharpDX.Direct2D1.DeviceContext D2dContext { get => _d2dContext; }

        private RasterizerStateDescription _rasterizerStateDescription;
        private RasterizerState _rasterizerState;

        private SharpDX.DXGI.Factory _factory;

        private SharpDX.Direct2D1.Factory _d2dFactory;
        public SharpDX.Direct2D1.Factory D2dFactory { get => _d2dFactory; }

        private Texture2D _backBuffer;
        public Texture2D BackBuffer { get => _backBuffer; }

        private RenderTargetView _renderTargetView;

        public RenderTargetView RenderTargetView { get => _renderTargetView; }

        private Texture2DDescription _depthStencilBufferDescription;

        private Texture2D _depthStencilBuffer;

        private DepthStencilView _depthStencilView;

        private Surface _surface;

        private bool _isFullScreen;
        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set
            {
                if(value != _isFullScreen)
                {
                    _isFullScreen = value;
                    _swapChain.SetFullscreenState(_isFullScreen, null);
                }
            }
        }

        private RenderTarget _d2dRenderTarget;

        public RenderTarget D2DRenderTarget { get => _d2dRenderTarget; }

        private SharpDX.DirectWrite.Factory _factoryDWrite;

        public SharpDX.DirectWrite.Factory FactoryDWrite { get => _factoryDWrite; }

        private SharpDX.WIC.ImagingFactory2 _imagingFactory;

        public SharpDX.WIC.ImagingFactory2 ImagingFactory { get => _imagingFactory; }

        public static Bitmap LoadFromFile(RenderTarget renderTarget, string file)
        {
            // Loads from file using System.Drawing.Image
            using (var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(file))
            {
                var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
                var size = new Size2(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }

                    }
                    bitmap.UnlockBits(bitmapData);
                    tempStream.Position = 0;

                    return new Bitmap(renderTarget, size, tempStream, stride, bitmapProperties);
                }
            }
        }
        public DirectX3DGraphics(RenderForm renderForm)
        {
            _renderForm = renderForm;

            Configuration.EnableObjectTracking = true;

            _sampleDescription = new SampleDescription(1, 0);

            _swapChainDescription = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(
                    _renderForm.ClientSize.Width,
                    _renderForm.ClientSize.Height,
                    new Rational(60, 1),
                    Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = _renderForm.Handle,
                SampleDescription = _sampleDescription,
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device11.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.BgraSupport,
                _swapChainDescription,
                out _device,
                out _swapChain);
            _deviceContext = _device.ImmediateContext;

            _rasterizerStateDescription = new RasterizerStateDescription()
            {
                FillMode = SharpDX.Direct3D11.FillMode.Solid,
                CullMode = CullMode.Back,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = true,
                IsAntialiasedLineEnabled = true,
                IsDepthClipEnabled = true
            };

            _rasterizerState = new RasterizerState(_device, _rasterizerStateDescription);
            _deviceContext.Rasterizer.State = _rasterizerState;

            _d2dFactory = new SharpDX.Direct2D1.Factory();

            int width = _renderForm.ClientSize.Width;
            int height = _renderForm.ClientSize.Height;

            _factory = _swapChain.GetParent<SharpDX.DXGI.Factory>();

            _factory.MakeWindowAssociation(_renderForm.Handle,
                WindowAssociationFlags.IgnoreAll);

            _depthStencilBufferDescription = new Texture2DDescription()
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = _renderForm.ClientSize.Width,
                Height = _renderForm.ClientSize.Height,
                SampleDescription = _sampleDescription,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            _renderTargetView = new RenderTargetView(_device, _backBuffer);


            _surface = _backBuffer.QueryInterface<Surface>();

            _d2dRenderTarget = new RenderTarget(_d2dFactory, _surface,
                new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied)));


            _dxgiDevice = _device.QueryInterface<SharpDX.DXGI.Device>();

            _d2dDevice = new SharpDX.Direct2D1.Device(_dxgiDevice);

            _imagingFactory = new SharpDX.WIC.ImagingFactory2();

            _d2dContext = new SharpDX.Direct2D1.DeviceContext(_d2dDevice, DeviceContextOptions.None);

            _factoryDWrite = new SharpDX.DirectWrite.Factory();



            // ???
            /*_dxgiDevice = _device.QueryInterface<SharpDX.DXGI.Device>();

            _d2dDevice = new SharpDX.Direct2D1.Device(_dxgiDevice);

            _d2dContext = new SharpDX.Direct2D1.DeviceContext(_d2dDevice, SharpDX.Direct2D1.DeviceContextOptions.None);

            var d2dPixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied);

            var d2dBitmapProps = new SharpDX.Direct2D1.BitmapProperties1(d2dPixelFormat, 96, 96, SharpDX.Direct2D1.BitmapOptions.Target | SharpDX.Direct2D1.BitmapOptions.CannotDraw);


            var d2dRenderTarget = new SharpDX.Direct2D1.Bitmap1(_d2dContext, new Size2(_renderForm.Width, _renderForm.Height), d2dBitmapProps);
            _d2dContext.Target = d2dRenderTarget; // associate bitmap with the d2d context*/

        }

        public void ChangeDisplayType(SharpDX.Direct3D11.FillMode fillMode)
        {
            _rasterizerStateDescription.FillMode = fillMode;

            _rasterizerState = new RasterizerState(_device, _rasterizerStateDescription);
            _deviceContext.Rasterizer.State = _rasterizerState;
        }


        public void Resize()
        {
            Utilities.Dispose(ref _depthStencilView);
            Utilities.Dispose(ref _depthStencilBuffer);
            Utilities.Dispose(ref _renderTargetView);
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _surface);
            Utilities.Dispose(ref _d2dRenderTarget);

            _swapChain.ResizeBuffers(_swapChainDescription.BufferCount,
                _renderForm.ClientSize.Width, _renderForm.ClientSize.Height,
                Format.Unknown, SwapChainFlags.None);


            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            _renderTargetView = new RenderTargetView(_device, _backBuffer);

            _depthStencilBufferDescription.Width = _renderForm.ClientSize.Width;

            _depthStencilBufferDescription.Height = _renderForm.ClientSize.Height;

            _depthStencilBuffer = new Texture2D(_device, _depthStencilBufferDescription);

            _depthStencilView = new DepthStencilView(_device, _depthStencilBuffer);

            _surface = _backBuffer.QueryInterface<Surface>();

            _d2dRenderTarget = new RenderTarget(_d2dFactory, _surface,
                new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied)));

            _deviceContext.Rasterizer.SetViewport(
                new Viewport(0, 0,
                _renderForm.ClientSize.Width, _renderForm.ClientSize.Height,
                0.0f, 1.0f)
                );
            _deviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
        }

        public void ClearBuffers(Color backgroundColor)
        {
            _deviceContext.ClearDepthStencilView(
                _depthStencilView,
                DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil,
                1.0f, 0);
            _deviceContext.ClearRenderTargetView(_renderTargetView, backgroundColor);
            //_d2dContext.Clear(backgroundColor);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _depthStencilView);
            Utilities.Dispose(ref _depthStencilBuffer);
            Utilities.Dispose(ref _renderTargetView);
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _factory);
            Utilities.Dispose(ref _rasterizerState);
            Utilities.Dispose(ref _deviceContext);
            Utilities.Dispose(ref _swapChain);
            Utilities.Dispose(ref _device);
        }
    }
}
