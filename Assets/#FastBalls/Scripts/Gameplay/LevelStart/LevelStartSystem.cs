using System;
using _FastBalls.Gameplay.GameState;
using Zenject;

namespace _FastBalls.Gameplay.LevelStart
{
    public class LevelStartSystem : IInitializable, IDisposable
    {
        private readonly LevelStartPresenter m_screenPresenter = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        
        
        [Inject]
        public LevelStartSystem(
            LevelStartPresenter screenPresenter,
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

            m_screenPresenter.StartButtonClicked += OnStartButtonClicked;
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            
            m_screenPresenter.StartButtonClicked -= OnStartButtonClicked;
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.LEVEL_START)
            {
                ShowScreen();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.LEVEL_START)
            {
                HideScreen();
            }
        }

        private void ShowScreen()
        {
            m_screenPresenter.ShowScreen();
        }

        private void HideScreen()
        {
            m_screenPresenter.HideScreen();
        }

        private void OnStartButtonClicked()
        {
            m_gameStateSystem.ChangeState(EGameState.GAMEPLAY);
        }
    }
}