using System;
using _FastBalls.Gameplay.LevelFinish.UI;
using Zenject;

namespace _FastBalls.Gameplay.LevelFinish
{
    public class LevelFinishPresenter
    {
        public event Action CloseButtonClicked = null; 


        private readonly UILevelFinishScreen m_screen = null;
        
        
        [Inject]
        public LevelFinishPresenter(
            UILevelFinishScreen screen
        )
        {
            m_screen = screen;
        }

        public void ShowScreen(LevelFinishData data)
        {
            m_screen.Show(true);
            m_screen.CloseButtonClicked += OnCloseButtonClicked;
            m_screen.ChangeScore(data.Score);
        }

        public void HideScreen()
        {
            m_screen.Hide(true);
            m_screen.CloseButtonClicked -= OnCloseButtonClicked;
        }

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}