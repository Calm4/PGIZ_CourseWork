using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public struct BoundingCapsule
    {
        public Vector3 Center;
        public float Radius;
        public float Height;

        public BoundingCapsule(Vector3 center, float radius, float height)
        {
            this.Center = center;
            this.Radius = radius;
            this.Height = height;
        }

        public bool Intersects(ref BoundingBox box)
        {
            // Находим ближайшую точку внутри Box к центру капсулы
            Vector3 closestPoint;
            Vector3.Clamp(ref this.Center, ref box.Minimum, ref box.Maximum, out closestPoint);

            // Проверяем, находится ли эта точка внутри капсулы
            float distanceSquared = Vector3.DistanceSquared(this.Center, closestPoint);
            bool intersects = distanceSquared <= this.Radius * this.Radius;

            // Если точка внутри капсулы, то Box пересекает капсулу
            if (intersects)
            {
                return true;
            }

            // Если точка не внутри капсулы, проверяем, находится ли она внутри цилиндра капсулы
            Vector3 cylinderAxisPoint = new Vector3(this.Center.X, closestPoint.Y, this.Center.Z);
            distanceSquared = Vector3.DistanceSquared(cylinderAxisPoint, closestPoint);
            intersects = distanceSquared <= this.Radius * this.Radius;

            // Если точка не внутри цилиндра, проверяем, находится ли она внутри полусфер на концах капсулы
            if (!intersects)
            {
                Vector3 topSphereCenter = new Vector3(this.Center.X, this.Center.Y + this.Height / 2, this.Center.Z);
                Vector3 bottomSphereCenter = new Vector3(this.Center.X, this.Center.Y - this.Height / 2, this.Center.Z);

                distanceSquared = Vector3.DistanceSquared(topSphereCenter, closestPoint);
                intersects = distanceSquared <= this.Radius * this.Radius;

                if (!intersects)
                {
                    distanceSquared = Vector3.DistanceSquared(bottomSphereCenter, closestPoint);
                    intersects = distanceSquared <= this.Radius * this.Radius;
                }
            }

            return intersects;
        }



    }


}
