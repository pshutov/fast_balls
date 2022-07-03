using System;
using System.Collections;
using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.Configs;
using _FastBalls.Gameplay.GameState;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.LevelTimer
{
    public class LevelTimerSystem : IInitializable, IDisposable
    {
        public float CurrentTime => m_levelTimerData.CurrentTime;
        public float LevelTime => m_levelTimerData.LevelTime;
        public float TimeProgress => 1f - (CurrentTime / LevelTime);
        
        
        private readonly LevelTimerPresenter m_screenPresenter = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly LevelConfig m_levelConfig = null;
        private readonly LevelCoroutinesProvider m_coroutinesProvider = null;


        private LevelTimerData m_levelTimerData = null;
        private Coroutine m_timerCoroutine = null;


        [Inject]
        public LevelTimerSystem(
            LevelTimerPresenter screenPresenter,
            GameStateSystem gameStateSystem,
            LevelConfig levelConfig,
            LevelCoroutinesProvider coroutinesProvider
        )
        {
            m_screenPresenter = screenPresenter;
            m_gameStateSystem = gameStateSystem;
            m_levelConfig = levelConfig;
            m_coroutinesProvider = coroutinesProvider;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            ChangeGameState(m_gameStateSystem.CurrentState);
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            
            StopTimerCoroutine();
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
                RunTimerCoroutine();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                HideScreen();
                StopTimerCoroutine();
            }
        }

        private void ShowScreen()
        {
            float levelTime = m_levelConfig.LevelTime;
            m_levelTimerData = new LevelTimerData(levelTime);
            m_screenPresenter.ShowScreen(m_levelTimerData);
        }

        private void HideScreen()
        {
            m_screenPresenter.HideScreen();
        }

        private void RunTimerCoroutine()
        {
            StopTimerCoroutine();

            m_timerCoroutine = 
                m_coroutinesProvider.StartCoroutine(TimerCoroutine(OnTimerEnd));
        }

        private void StopTimerCoroutine()
        {
            if (m_timerCoroutine != null)
            {
                if (m_coroutinesProvider != null)
                {
                    m_coroutinesProvider.StopCoroutine(m_timerCoroutine);
                }
                m_timerCoroutine = null;
            }   
        }

        private IEnumerator TimerCoroutine(Action callback)
        {
            while (m_levelTimerData.CurrentTime > 0f)
            {
                m_levelTimerData.DecTime(Time.deltaTime);
                
                yield return null;
            }
            
            callback.Invoke();
        }

        private void OnTimerEnd()
        {
            m_gameStateSystem.ChangeState(EGameState.LEVEL_FINISH);
        }
    }
}