using Lab01.App.Scripts.Environment;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Scripts.Game
{
    public static class GameLights
    {
        public static LightProperties Light;
        private const int NUM_LIGHTS = 4;

        private static Vector4[] _lightColors = new Vector4[NUM_LIGHTS]
       {
            new Vector4(0f, 1f, 1f, 1f),
            new Vector4(0f, 1f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(0.5f, 0f, 0f, 1f)
       };

        private static int[] _lighTypes = new int[NUM_LIGHTS]
        {
            1,
            0,
            1,
            0,
        };

        private static int[] _lightEnabled = new int[NUM_LIGHTS]
        {
            1,
            1,
            1,
            1
        };


        public static void CreateLights()
        {
            Light.Lights = new Light[NUM_LIGHTS];
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

                Light.Lights[i] = light;
            }

            Light.GlobalAmbient = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
        }
    }
}
