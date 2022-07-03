using System;
using _FastBalls.Gameplay.GameState;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.InputClicker
{
    public class InputClickerSystem : IInitializable, IDisposable
    {
        public event Action<Vector3> Clicked = null; 
        
        
        private readonly InputClickerPresenter m_screenPresenter = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        
        
        [Inject]
        public InputClickerSystem(
            InputClickerPresenter screenPresenter,
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

            m_screenPresenter.Clicked += OnClicked;
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            
            m_screenPresenter.Clicked -= OnClicked;
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
            m_screenPresenter.ShowScreen();
        }

        private void HideScreen()
        {
            m_screenPresenter.HideScreen();
        }

        private void OnClicked(Vector3 worldPosition)
        {
            Clicked?.Invoke(worldPosition);
        }
    }
}