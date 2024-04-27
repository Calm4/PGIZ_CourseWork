using System.Runtime.InteropServices;
using SharpDX;

namespace Lab01.App.Scripts.Environment
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Light
    {
        public Vector4 Position;
        public Vector4 Direction;
        public Vector4 Color;

        public float SpotAngle;
        public float ConstantAttenuation;
        public float LinearAttenuation;
        public float QuadraticAttenuation;

        public int LightType;
        public int Enabled;
        public Vector2 Padding;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LightProperties
    {
        public Vector4 EyePosition;
        public Vector4 GlobalAmbient;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Light[] Lights;
    }
}
