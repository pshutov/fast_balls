using System;
using _FastBalls.Gameplay.GameState;
using Zenject;

namespace _FastBalls.Gameplay.LevelScore
{
    public class LevelScoreSystem : IInitializable, IDisposable
    {
        public int Score => m_levelScoreData.Score;
        
        
        private readonly LevelScorePresenter m_screenPresenter = null;
        private readonly GameStateSystem m_gameStateSystem = null;


        private LevelScoreData m_levelScoreData = null;


        [Inject]
        public LevelScoreSystem(
            LevelScorePresenter screenPresenter,
            GameStateSystem gameStateSystem
        )
        {
            m_screenPresenter = screenPresenter;
            m_gameStateSystem = gameStateSystem;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            ChangeGameState(m_gameStateSystem.CurrentState);
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
        }

        public void IncScore(int score)
        {
            m_levelScoreData.IncScore(score);
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.GAMEPLAY)
            {
                ShowScreen();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                HideScreen();
            }
        }

        private void ShowScreen()
        {
            m_levelScoreData = new LevelScoreData();
            m_screenPresenter.ShowScreen(m_levelScoreData);
        }

        private void HideScreen()
        {
            m_screenPresenter.HideScreen();
        }
    }
}