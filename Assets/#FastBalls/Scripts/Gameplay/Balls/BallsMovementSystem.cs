using System;
using System.Collections;
using _CrossProjects.Common.Cameras;
using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.GameState;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsMovementSystem : IInitializable, IDisposable
    {
        private readonly BallsContainer m_ballsContainer = null;
        private readonly LevelCoroutinesProvider m_coroutinesProvider = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly CamerasController m_camerasController = null;


        private Coroutine m_movementCoroutine = null;
        
        
        [Inject]
        public BallsMovementSystem(
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

            m_ballsContainer.Added += OnBallAdded;
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;

            m_ballsContainer.Added -= OnBallAdded;
            
            StopMovementCoroutine();
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.GAMEPLAY)
            {
                RunMovementCoroutine();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                StopMovementCoroutine();
            }
        }

        private void RunMovementCoroutine()
        {
            StopMovementCoroutine();
            
            m_movementCoroutine = 
                m_coroutinesProvider.StartCoroutine(MovementCoroutine());
        }

        private void StopMovementCoroutine()
        {
            if (m_movementCoroutine != null)
            {
                if (m_coroutinesProvider != null)
                {
                    m_coroutinesProvider.StopCoroutine(m_movementCoroutine);
                }
                m_movementCoroutine = null;
            }   
        }

        private IEnumerator MovementCoroutine()
        {
            yield return null;
            
            while (true)
            {
                while (!IsBallsCreated())
                {
                    yield return null;
                }

                MoveBalls(Time.deltaTime);
                
                yield return null;
            }
        }

        private bool IsBallsCreated()
        {
            int ballsCount = m_ballsContainer.Count;
            return ballsCount != 0;
        }

        private void MoveBalls(float deltaTime)
        {
            int count = m_ballsContainer.Count;
            for (int i = 0; i < count; i++)
            {
                var ballController = m_ballsContainer[i];
                if (!ballController.IsActive)
                {
                    continue;
                }

                MoveBall(ballController, deltaTime);
            }
        }

        private void MoveBall(BallController ballController, float deltaTime)
        {
            var ballPosition = ballController.Position;
            float ballSpeed = ballController.Speed;
            ballPosition.y += ballSpeed * deltaTime;
            ballController.SetPosition(ballPosition);
        }

        private void OnBallAdded(BallController ballController)
        {
            var position = GetNextPositionForBall(ballController);
            ballController.SetPosition(position);
        }

        private Vector3 GetNextPositionForBall(BallController ballController)
        {
            if (m_ballsContainer.Count > 2)
            {
                return GetPositionAfterBalls(ballController);
            }
            else
            {
                return GetFirstPositionForBall(ballController);
            }
        }

        private Vector3 GetPositionAfterBalls(BallController ballController)
        {
            var lastBallController = m_ballsContainer[m_ballsContainer.Count - 2];
            var lastBallPosition = lastBallController.Position;
            float lastBallRadius = lastBallController.Radius;
            
            float ballRadius = ballController.Radius;
            var screenRect = GetScreenRect();
            float positionX = UnityEngine.Random.Range(screenRect.xMin + ballRadius, screenRect.xMax - ballRadius);
            float yMin = Mathf.Min(lastBallPosition.y - lastBallRadius, screenRect.yMin);
            float positionY = yMin - ballRadius;
            return new Vector3(positionX, positionY);
        }

        private Vector3 GetFirstPositionForBall(BallController ballController)
        {
            float ballRadius = ballController.Radius;
            var screenRect = GetScreenRect();
            float positionX = UnityEngine.Random.Range(screenRect.xMin + ballRadius, screenRect.xMax - ballRadius);
            float positionY = screenRect.yMin - ballRadius;
            return new Vector3(positionX, positionY);
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