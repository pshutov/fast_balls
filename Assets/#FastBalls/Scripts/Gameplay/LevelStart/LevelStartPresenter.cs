using System;
using _FastBalls.Gameplay.LevelStart.UI;
using Zenject;

namespace _FastBalls.Gameplay.LevelStart
{
    public class LevelStartPresenter
    {
        public event Action StartButtonClicked = null; 


        private readonly UILevelStartScreen m_screen = null;
        
        
        [Inject]
        public LevelStartPresenter(
            UILevelStartScreen screen
        )
        {
            m_screen = screen;
        }

        public void ShowScreen()
        {
            m_screen.Show(false);
            m_screen.StartButtonClicked += OnStartButtonClicked;
        }

        public void HideScreen()
        {
            m_screen.Hide(true);
            m_screen.StartButtonClicked -= OnStartButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            StartButtonClicked?.Invoke();
        }
    }
}