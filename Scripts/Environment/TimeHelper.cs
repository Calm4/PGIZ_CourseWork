using System;
using System.Diagnostics;

namespace Lab01.App.Scripts.Environment
{
    public class TimeHelper
    {
        private Stopwatch _stopwatch;

        private int _framesCounter = 0;

        private int _fps = 0;
        public int FPS { get => _fps; }

        private long _previousFPSMeasurementTime;

        private long _previousTicks;

        private float _time;
        public float Time { get => _time; }

        private float _deltaT;
        public float DeltaT { get => _deltaT; }

        public TimeHelper()
        {
            _stopwatch = new Stopwatch();
            Reset();
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _framesCounter = 0;
            _fps = 0;
            _stopwatch.Start();
            _previousFPSMeasurementTime = _stopwatch.ElapsedMilliseconds;
            _previousTicks = _stopwatch.Elapsed.Ticks;
        }

        public void Update()
        {
            long ticks = _stopwatch.Elapsed.Ticks;
            _time = (float)ticks / TimeSpan.TicksPerSecond;
            _deltaT = (float)(ticks - _previousTicks) / TimeSpan.TicksPerSecond;
            _previousTicks = ticks;

            _framesCounter++;

            if(_stopwatch.ElapsedMilliseconds - _previousFPSMeasurementTime >= 1000)
            {
                _fps = _framesCounter;
                _framesCounter = 0;
                _previousFPSMeasurementTime = _stopwatch.ElapsedMilliseconds;
            }
        }
    }
}
