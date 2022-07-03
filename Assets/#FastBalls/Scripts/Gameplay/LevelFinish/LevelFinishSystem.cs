using System;
using _FastBalls.Gameplay.GameState;
using _FastBalls.Gameplay.LevelFinish.UI;
using _FastBalls.Gameplay.LevelScore;
using Zenject;

namespace _FastBalls.Gameplay.LevelFinish
{
    public class LevelFinishSystem : IInitializable, IDisposable
    {
        private readonly LevelFinishPresenter m_screenPresenter = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly LevelScoreSystem m_levelScoreSystem = null;


        [Inject]
        public LevelFinishSystem(
            LevelFinishPresenter screenPresenter,
            GameStateSystem gameStateSystem,
            LevelScoreSystem levelScoreSystem
        )
        {
            m_screenPresenter = screenPresenter;
            m_gameStateSystem = gameStateSystem;
            m_levelScoreSystem = levelScoreSystem;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            ChangeGameState(m_gameStateSystem.CurrentState);

            m_screenPresenter.CloseButtonClicked += OnCloseButtonClicked;
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;

            m_screenPresenter.CloseButtonClicked -= OnCloseButtonClicked;
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.LEVEL_FINISH)
            {
                ShowScreen();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.LEVEL_FINISH)
            {
                HideScreen();
            }
        }

        private void ShowScreen()
        {
            var score = m_levelScoreSystem.Score;
            var levelFinishData = new LevelFinishData(score);
            m_screenPresenter.ShowScreen(levelFinishData);
        }

        private void HideScreen()
        {
            m_screenPresenter.HideScreen();
        }

        private void OnCloseButtonClicked()
        {
            m_gameStateSystem.ChangeState(EGameState.GAMEPLAY);
        }
    }
}