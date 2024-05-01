using Lab01.App.Scripts.DirectX;
using Lab01.App.Scripts.Environment;
using SharpDX;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Scripts.Game
{
    public static class GameInputs
    {
        private static Vector3 playerMovement = Vector3.Zero;
        private static DXInput _dxInput;
        private static Camera _camera;

        public static void InitializeConstruct(DXInput dxInput, Camera camera)
        {
            _dxInput = dxInput;
            _camera = camera;
        }

        public static Vector3 InputMovement()
        {

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

    }
}
