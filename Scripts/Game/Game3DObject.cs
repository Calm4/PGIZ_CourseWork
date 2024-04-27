using SharpDX;

namespace Lab01.App.Scripts.Game
{
    class Game3DObject
    {
        internal Vector4 _position;
        public Vector4 Position { get => _position; }

        internal float _yaw;
        public float Yaw { get => _yaw; set => _yaw = value; }

        internal float _pitch;
        public float Pitch { get => _pitch; set => _pitch = value; }

        internal float _roll;
        public float Roll { get => _roll; set => _roll = value; }

        public Game3DObject(Vector4 position, float yaw = 0.0f, float pitch = 0.0f, float roll = 0.0f)
        {
            _position = position;
            _yaw = yaw;
            _pitch = pitch;
            _roll = roll;
        }

        private void LimitAngleByPlusMinusPi(ref float angle)
        {
            if (angle > MathUtil.Pi) angle -= MathUtil.TwoPi;
            else if (angle < -MathUtil.Pi) angle += MathUtil.TwoPi;
        }

        public virtual void YawBy(float deltaYaw)
        {
            _yaw += deltaYaw;
            LimitAngleByPlusMinusPi(ref _yaw);
        }

        public virtual void PitchBy(float deltaPitch)
        {
            _pitch += deltaPitch;
            LimitAngleByPlusMinusPi(ref _pitch);
        }

        public virtual void RollBy(float deltaRoll)
        {
            _roll += deltaRoll;
            LimitAngleByPlusMinusPi(ref _roll);
        }

        public virtual void MoveBy(float deltaX, float deltaY, float deltaZ) 
        {
            _position.X += deltaX;
            _position.Y += deltaY;
            _position.Z += deltaZ;
        }

        public virtual void MoveTo(float x, float y, float z)
        {
            _position.X = x;
            _position.Y = y;
            _position.Z = z;
        }

        public Matrix GetWorldMatrix()
        {
            return Matrix.Multiply(Matrix.RotationYawPitchRoll(_yaw, _pitch, _roll), Matrix.Translation((Vector3)_position));
        }
    }
}
