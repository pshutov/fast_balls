using System;
using _FastBalls.Gameplay.LevelScore;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsScoresSystem : IInitializable, IDisposable
    {
        private readonly BallsClickerSystem m_ballsClickerSystem;
        private readonly  LevelScoreSystem m_levelScoreSystem;


        [Inject]
        public BallsScoresSystem(
            BallsClickerSystem ballsClickerSystem,
            LevelScoreSystem levelScoreSystem
        )
        {
            m_ballsClickerSystem = ballsClickerSystem;
            m_levelScoreSystem = levelScoreSystem;
        }

        void IInitializable.Initialize()
        {
            m_ballsClickerSystem.Clicked += OnBallClicked;
        }

        void IDisposable.Dispose()
        {
            m_ballsClickerSystem.Clicked -= OnBallClicked;
        }

        private void OnBallClicked(BallController ballController)
        {
            int ballScore = ballController.Score;
            m_levelScoreSystem.IncScore(ballScore);
            ballController.SetActive(false);
        }
    }
}