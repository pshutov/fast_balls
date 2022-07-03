using System;

namespace _FastBalls.Gameplay.LevelTimer
{
    public class LevelTimerData
    {
        public event Action<float, float> TimeChanged = null; 


        public readonly float LevelTime = 0;
        
        
        public float CurrentTime { get; private set; }


        public LevelTimerData(float levelTime)
        {
            LevelTime = levelTime;
            CurrentTime = levelTime;
        }

        public float DecTime(float delta)
        {
            float time = CurrentTime - delta;
            SetTime(time);
            return time;
        }

        private void SetTime(float value)
        {
            CurrentTime = value;
            TimeChanged?.Invoke(value, LevelTime);
        }
    }
}