using _FastBalls.Gameplay.LevelScore.UI;
using Zenject;

namespace _FastBalls.Gameplay.LevelScore
{
    public class LevelScorePresenter
    {
        private readonly UILevelScoreScreen m_screen = null;


        private LevelScoreData m_data = null;


        [Inject]
        public LevelScorePresenter(
            UILevelScoreScreen screen
        )
        {
            m_screen = screen;
        }

        public void ShowScreen(LevelScoreData data)
        {
            if (m_data != null)
            {
                data.ScoreChanged -= OnScoreChanged;
            }
            
            m_data = data;
            
            if (data != null)
            {
                m_screen.Show(true);
                
                data.ScoreChanged += OnScoreChanged;
                ChangeScore(data.Score);
            }
        }

        public void HideScreen()
        {
            m_screen.Hide(true);
        }

        private void OnScoreChanged(int score)
        {
            ChangeScore(score);
        }

        private void ChangeScore(int score)
        {
            m_screen.ChangeScore(score);
        }
    }
}