using System;
using System.Windows.Forms;
using SharpDX.Direct3D;
using Device11 = SharpDX.Direct3D11.Device;

namespace Lab01.App.Scripts
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(!(Device11.GetSupportedFeatureLevel() == FeatureLevel.Level_11_0))
            {
                MessageBox.Show("DirectX11 Not Supported");
                return;
            }

            Game.Game game = new Game.Game();
            game.Run();
            game.Dispose();
        }
    }
}
