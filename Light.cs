using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
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
