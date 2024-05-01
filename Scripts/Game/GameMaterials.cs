using Lab01.App.Scripts.Textures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Scripts
{
    public static class GameMaterials
    {
        public static MaterialProperties DefaultMaterial;

        public static MaterialProperties FloorMaterial;

        public static MaterialProperties BlackMaterial;

        public static MaterialProperties IcosahedronMaterial;

        public static MaterialProperties CurrentTetrahedronMaterial;
        public static MaterialProperties CurrentIcosahedronMaterial;

        public static MaterialProperties ContactingMaterial;

        public static MaterialProperties DefaultObjectMaterial;
        public static MaterialProperties ChangedObjectMaterial;
        public static MaterialProperties CurrentObjectMaterial;


        public static MaterialProperties RayMaterial;

        public static void InitializeMaterials()
        {
            DefaultMaterial = new MaterialProperties
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

            FloorMaterial = new MaterialProperties
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


            BlackMaterial = new MaterialProperties
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


            IcosahedronMaterial = new MaterialProperties
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

            ContactingMaterial = new MaterialProperties
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

            RayMaterial = new MaterialProperties
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

            CurrentTetrahedronMaterial = DefaultMaterial;

            CurrentIcosahedronMaterial = IcosahedronMaterial;

            CurrentObjectMaterial = FloorMaterial;

            CurrentObjectMaterial = FloorMaterial;
        }

    }
}
