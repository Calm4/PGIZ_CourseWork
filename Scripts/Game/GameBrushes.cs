using Lab01.App.Scripts.DirectX;
using SharpDX;
using SharpDX.Direct2D1;


namespace Lab01.Scripts.Game
{
    public static class GameBrushes
    {
        public static SolidColorBrush GreenBrush;

        public static SolidColorBrush RedBrush;

        public static SolidColorBrush BlueBrush;

        public static SolidColorBrush PurpleBrush;

        public static SolidColorBrush WhiteBrush;

        public static SolidColorBrush SecondGreenBrush;

        public static SolidColorBrush BlackBrush;

        public static SolidColorBrush SecondBlackBrush;

        public static SolidColorBrush DanilkaBrush;


        public static SolidColorBrush semiTransparentUIBrush;

        public static void InitializeBrushes(DirectX3DGraphics directX3DGraphics)
        {
            var green = Color.Green;

            var semiTransparent = Color.Wheat;
            semiTransparent.A = 128; // полупрозрачный

            semiTransparentUIBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, semiTransparent);


            green.A = 100;

            var white = Color.WhiteSmoke;
            white.A = 70;

            var black = Color.Black;
            black.A = 100;

            var gg = new Color(244, 230, 203);
            gg.A = 128;

            GreenBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, green);
            RedBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, Color.Red);
            BlueBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, Color.Blue);
            PurpleBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, Color.Purple);
            WhiteBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, Color.WhiteSmoke);

            green = Color.Green;
            SecondGreenBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, green);
            BlackBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, black);
            DanilkaBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, gg);
            SecondBlackBrush = new SolidColorBrush(directX3DGraphics.D2DRenderTarget, Color.Black);
        }
    }
}
