using System;
using System.Collections;
using _CrossProjects.Common.Cameras;
using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.GameState;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsOutsideCheckerSystem : IInitializable, IDisposable
    {
        private readonly BallsContainer m_ballsContainer = null;
        private readonly LevelCoroutinesProvider m_coroutinesProvider = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly CamerasController m_camerasController = null;


        private Coroutine m_checkerCoroutine = null;
        
        
        [Inject]
        public BallsOutsideCheckerSystem(
            BallsContainer ballsContainer,
            LevelCoroutinesProvider coroutinesProvider,
            GameStateSystem gameStateSystem,
            CamerasController camerasController
        )
        {
            m_ballsContainer = ballsContainer;
            m_coroutinesProvider = coroutinesProvider;
            m_gameStateSystem = gameStateSystem;
            m_camerasController = camerasController;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            ChangeGameState(m_gameStateSystem.CurrentState);
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            
            StopCheckerCoroutine();
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.GAMEPLAY)
            {
                RunCheckerCoroutine();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                StopCheckerCoroutine();
            }
        }

        private void RunCheckerCoroutine()
        {
            StopCheckerCoroutine();
            
            m_checkerCoroutine = 
                m_coroutinesProvider.StartCoroutine(CheckerCoroutine());
        }

        private void StopCheckerCoroutine()
        {
            if (m_checkerCoroutine != null)
            {
                if (m_coroutinesProvider != null)
                {
                    m_coroutinesProvider.StopCoroutine(m_checkerCoroutine);
                }
                m_checkerCoroutine = null;
            }   
        }

        private IEnumerator CheckerCoroutine()
        {
            yield return null;
            
            while (true)
            {
                while (!IsBallsCreated())
                {
                    yield return null;
                }

                CheckBalls();
                
                yield return null;
            }
        }

        private bool IsBallsCreated()
        {
            int ballsCount = m_ballsContainer.Count;
            return ballsCount != 0;
        }

        private void CheckBalls()
        {
            int count = m_ballsContainer.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                var ballController = m_ballsContainer[i];
                if (!ballController.IsActive)
                {
                    continue;
                }

                CheckBall(ballController);
            }
        }

        private void CheckBall(BallController ballController)
        {
            var ballPosition = ballController.Position;
            float ballRadius = ballController.Radius;
            
            var screenRect = GetScreenRect();
            
            float ballBottom = ballPosition.y - ballRadius;
            float screenTop = screenRect.yMax;

            if (ballBottom > screenTop)
            {
                ballController.SetActive(false);
            }
        }

        private Rect GetScreenRect()
        {
            var gameCameraController = m_camerasController.GetCamera(ECameraId.GAME);
            var gameCamera = gameCameraController.Camera;
            var bottomLeft = gameCamera.ViewportToWorldPoint(Vector3.zero);
            // var topLeft = gameCamera.ViewportToWorldPoint(Vector3.up);
            // var bottomRight = gameCamera.ViewportToWorldPoint(Vector3.right);
            var topRight = gameCamera.ViewportToWorldPoint(Vector3.one);

            return new Rect(bottomLeft, topRight - bottomLeft);
        }
    }
}