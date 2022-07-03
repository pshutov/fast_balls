using _FastBalls.Gameplay.LevelTimer.UI;
using Zenject;

namespace _FastBalls.Gameplay.LevelTimer
{
    public class LevelTimerPresenter
    {
        private readonly UILevelTimerScreen m_screen = null;


        private LevelTimerData m_data = null;


        [Inject]
        public LevelTimerPresenter(
            UILevelTimerScreen screen
        )
        {
            m_screen = screen;
        }

        public void ShowScreen(LevelTimerData data)
        {
            if (m_data != null)
            {
                data.TimeChanged -= OnTimeChanged;
            }
            
            m_data = data;
            
            if (data != null)
            {
                m_screen.Show(true);
                
                data.TimeChanged += OnTimeChanged;
                ChangeTimeValue(data.CurrentTime, data.LevelTime);
            }
        }

        public void HideScreen()
        {
            m_screen.Hide(true);
        }

        private void OnTimeChanged(float time, float levelTime)
        {
            ChangeTimeValue(time, levelTime);
        }

        private void ChangeTimeValue(float time, float levelTime)
        {
            m_screen.ChangeTimeValue(time, levelTime);
        }
    }
}