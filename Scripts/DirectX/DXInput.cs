using System;
using SharpDX;
using SharpDX.DirectInput;

namespace Lab01.App.Scripts.DirectX
{
    public class DXInput : IDisposable
    {
        private DirectInput _directInput;

        private Keyboard _keyboard;
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        private Mouse _mouse;
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private Point _currentMouse;

        public DXInput(IntPtr windowHandle)
        {
            _directInput = new DirectInput();

            _keyboard = new Keyboard(_directInput);
            _currentKeyboardState = new KeyboardState();
            _previousKeyboardState = new KeyboardState();
            _keyboard.Acquire();

            _mouse = new Mouse(_directInput);
            _currentMouseState = new MouseState();
            _previousMouseState = new MouseState();
            _mouse.Acquire();
        }

        public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = _keyboard.GetCurrentState();

            MouseState previousState = _currentMouseState;
            _currentMouseState = _mouse.GetCurrentState();

            if(_currentMouseState.X != previousState.X || _currentMouseState.Y != previousState.Y)
            {
                _currentMouse.X = _currentMouseState.X;
                _currentMouse.Y = _currentMouseState.Y;
            }
        }

        public bool IsKeyPressed(Key key)
        {
            return _currentKeyboardState.IsPressed(key);
        }

        public bool IsKeyReleased(Key key)
        {
            return !_currentKeyboardState.IsPressed(key) && _previousKeyboardState.IsPressed(key);
        }

        public int GetMouseDeltaX()
        {
            return _currentMouseState.X - _previousMouseState.X;
        }
        public int GetMouseDeltaY()
        {
            return _currentMouseState.Y - _previousMouseState.Y;
        }

        public void Dispose()
        {
            _keyboard.Unacquire();
            _keyboard.Dispose();
            _mouse.Unacquire();
            _mouse.Dispose();
            _directInput.Dispose();
        }
    }
}
