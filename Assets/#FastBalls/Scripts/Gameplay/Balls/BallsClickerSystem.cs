using System;
using System.Collections;
using System.Collections.Generic;
using _CrossProjects.Common.Cameras;
using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.GameState;
using _FastBalls.Gameplay.InputClicker;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsClickerSystem : IInitializable, IDisposable
    {
        public event Action<BallController> Clicked = null;
        
        
        private readonly BallsContainer m_ballsContainer = null;
        private readonly LevelCoroutinesProvider m_coroutinesProvider = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly InputClickerSystem m_inputClickerSystem = null;

        
        private readonly List<Vector3> m_clickedPositions = new List<Vector3>();


        private Coroutine m_clickerCoroutine = null;
        
        
        [Inject]
        public BallsClickerSystem(
            BallsContainer ballsContainer,
            LevelCoroutinesProvider coroutinesProvider,
            GameStateSystem gameStateSystem,
            InputClickerSystem inputClickerSystem
        )
        {
            m_ballsContainer = ballsContainer;
            m_coroutinesProvider = coroutinesProvider;
            m_gameStateSystem = gameStateSystem;
            m_inputClickerSystem = inputClickerSystem;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            m_inputClickerSystem.Clicked += OnClicked;
            ChangeGameState(m_gameStateSystem.CurrentState);
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            m_inputClickerSystem.Clicked -= OnClicked;
            
            StopClickerCoroutine();
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.GAMEPLAY)
            {
                RunClickerCoroutine();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                StopClickerCoroutine();
            }
        }

        private void RunClickerCoroutine()
        {
            StopClickerCoroutine();
            
            m_clickerCoroutine = 
                m_coroutinesProvider.StartCoroutine(ClickerCoroutine());
        }

        private void StopClickerCoroutine()
        {
            if (m_clickerCoroutine != null)
            {
                if (m_coroutinesProvider != null)
                {
                    m_coroutinesProvider.StopCoroutine(m_clickerCoroutine);
                }
                m_clickerCoroutine = null;
            }   
        }

        private IEnumerator ClickerCoroutine()
        {
            yield return null;
            
            while (true)
            {
                while (m_clickedPositions.Count == 0 || !IsBallsCreated())
                {
                    yield return null;
                }

                CheckIfClickedAtBalls(m_clickedPositions);
                m_clickedPositions.Clear();

                yield return null;
            }
        }

        private bool IsBallsCreated()
        {
            int ballsCount = m_ballsContainer.Count;
            return ballsCount != 0;
        }

        private void CheckIfClickedAtBalls(List<Vector3> clickedPositions)
        {
            int ballsCount = m_ballsContainer.Count;
            for (int i = ballsCount - 1; i >= 0; i--)
            {
                var ballController = m_ballsContainer[i];

                if (!ballController.IsActive || ballController.IsClicked)
                {
                    continue;
                }
                
                bool isClicked = CheckIfClickedAtBall(clickedPositions, ballController);

                if (isClicked)
                {
                    MarkBallAsClicked(ballController);
                }
            }
        }

        private bool CheckIfClickedAtBall(List<Vector3> clickedPositions, BallController ballController)
        {
            for (int i = clickedPositions.Count - 1; i >= 0; i--)
            {
                var clickedPosition = clickedPositions[i];
                if (CheckIfClickedAtBall(clickedPosition, ballController))
                {
                    clickedPositions.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfClickedAtBall(Vector3 clickedPosition, BallController ballController)
        {
            var ballPosition = ballController.Position;
            float ballRadius = ballController.Radius;
            bool isClicked = (Vector2.Distance(ballPosition, clickedPosition) <= ballRadius);
            return isClicked;
        }

        private void MarkBallAsClicked(BallController ballController)
        {
            ballController.SetClicked(true);
            Clicked?.Invoke(ballController);
        }

        private void OnClicked(Vector3 worldPosition)
        {
            m_clickedPositions.Add(worldPosition);
        }
    }
}