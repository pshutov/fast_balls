using System;
using System.Collections;
using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.Balls.Configs;
using _FastBalls.Gameplay.Configs;
using _FastBalls.Gameplay.GameState;
using _FastBalls.Gameplay.LevelTimer;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsCreatorSystem : IInitializable, IDisposable
    {
        private readonly BallsConfig m_ballsConfig = null;
        private readonly BallsContainer m_ballsContainer = null;
        private readonly GameStateSystem m_gameStateSystem = null;
        private readonly LevelCoroutinesProvider m_coroutinesProvider = null;
        private readonly BallsSpawnerSystem m_ballsSpawnerSystem = null;
        private readonly LevelTimerSystem m_levelTimerSystem = null;
        private readonly LevelConfig m_levelConfig = null;


        private int m_lastBallId = -1;
        private Coroutine m_creatorCoroutine = null;
        
        
        [Inject]
        public BallsCreatorSystem(
            BallsConfig ballsConfig,
            BallsContainer ballsContainer,
            GameStateSystem gameStateSystem,
            LevelCoroutinesProvider coroutinesProvider,
            BallsSpawnerSystem ballsSpawnerSystem,
            LevelTimerSystem levelTimerSystem,
            LevelConfig levelConfig
        )
        {
            m_ballsConfig = ballsConfig;
            m_ballsContainer = ballsContainer;
            m_gameStateSystem = gameStateSystem;
            m_coroutinesProvider = coroutinesProvider;
            m_ballsSpawnerSystem = ballsSpawnerSystem;
            m_levelTimerSystem = levelTimerSystem;
            m_levelConfig = levelConfig;
        }

        void IInitializable.Initialize()
        {
            m_gameStateSystem.StateChanged += OnGameStateChanged;
            ChangeGameState(m_gameStateSystem.CurrentState);
        }

        void IDisposable.Dispose()
        {
            m_gameStateSystem.StateChanged -= OnGameStateChanged;
            
            StopCreatorCoroutine();
        }

        private void OnGameStateChanged(EGameState state)
        {
            ChangeGameState(state);
        }

        private void ChangeGameState(EGameState state)
        {
            if (state == EGameState.GAMEPLAY)
            {
                RunCreatorCoroutine();
            }
            else if (m_gameStateSystem.PreviousState == EGameState.GAMEPLAY)
            {
                StopCreatorCoroutine();
                DisableCreatedBalls();
            }
        }

        private void DisableCreatedBalls()
        {
            int count = m_ballsContainer.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                var ballController = m_ballsContainer[i];
                ballController.SetActive(false);
            }
        }

        private void RunCreatorCoroutine()
        {
            StopCreatorCoroutine();
            
            m_creatorCoroutine = 
                m_coroutinesProvider.StartCoroutine(CreatorCoroutine());
        }

        private void StopCreatorCoroutine()
        {
            if (m_creatorCoroutine != null)
            {
                if (m_coroutinesProvider != null)
                {
                    m_coroutinesProvider.StopCoroutine(m_creatorCoroutine);
                }
                m_creatorCoroutine = null;
            }   
        }

        private IEnumerator CreatorCoroutine()
        {
            yield return null;
            
            while (true)
            {
                float delay = GetDelayForNextSpawn();
                
                yield return new WaitForSeconds(delay);
                
                CreateNewBall();
            }
        }

        private void CreateNewBall()
        {
            var ballData = CreateBallData();
            var ballController = m_ballsSpawnerSystem.SpawnBall(ballData);
            ballController.ActiveChanged += OnBallActiveStateChanged;
            m_ballsContainer.Add(ballController);
            ballController.SetActive(true);
        }

        private void OnBallActiveStateChanged(BallController ballController, bool isActive)
        {
            if (isActive)
            {
                return;
            }
            
            DestroyBall(ballController);
        }

        private void DestroyBall(BallController ballController)
        {
            m_ballsSpawnerSystem.DespawnBall(ballController);
            m_ballsContainer.Remove(ballController);
        }

        private BallData CreateBallData()
        {
            int id = ++m_lastBallId;
            float ballDifficult = GetNextBallDifficult();
            float ballRadius = GetBallRadius(ballDifficult);
            float ballSpeed = GetBallSpeed(ballDifficult);
            int ballScore = GetBallScore(ballDifficult);
            var ballColor = GetBallColor(ballDifficult);
            var ballData = new BallData(id, ballRadius, ballColor, ballScore, ballSpeed);
            
            return ballData;
        }

        private float GetNextBallDifficult()
        {
            return Random.value;
        }

        private float GetBallRadius(float difficult)
        {
            var range = m_ballsConfig.RadiusRange;
            // return Mathf.Lerp(range.x, range.y, difficult);
            return range.Evaluate(difficult);
        }

        private int GetBallScore(float difficult)
        {
            var range = m_ballsConfig.ScoreRange;
            return (int)Mathf.Lerp(range.x, range.y, difficult);
        }

        private Color GetBallColor(float difficult)
        {
            var gradient = m_ballsConfig.ColorGradient;
            return gradient.Evaluate(difficult);
        }

        private float GetBallSpeed(float difficult)
        {
            float speedFactor = GetBallSpeedFactor(difficult);
            float currentSpeed = GetCurrentLevelSpeed();
            return currentSpeed * speedFactor;
        }

        private float GetBallSpeedFactor(float difficult)
        {
            var range = m_ballsConfig.SpeedFactorRange;
            // return Mathf.Lerp(range.x, range.y, difficult);
            return range.Evaluate(difficult);
        }

        private float GetCurrentLevelSpeed()
        {
            float progress = m_levelTimerSystem.TimeProgress;
            var range = m_levelConfig.SpeedRange;
            return Mathf.Lerp(range.x, range.y, progress);
        }

        private float GetDelayForNextSpawn()
        {
            float progress = m_levelTimerSystem.TimeProgress;
            var range = m_levelConfig.SpawnDelayRange;
            return Mathf.Lerp(range.x, range.y, progress);
        }
    }
}