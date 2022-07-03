using System;
using System.Collections.Generic;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallsSpawnerSystem
    {
        private readonly BallController.Pool m_ballControllerPool = null;
        private readonly BallPresenter.Pool m_ballPresenterPool = null;
        private readonly BallView.Pool m_ballViewPool = null;


        private readonly List<BallContainer> m_balls = new List<BallContainer>();


        [Inject]
        public BallsSpawnerSystem(
            BallController.Pool ballControllerPool,
            BallPresenter.Pool ballPresenterPool,
            BallView.Pool ballViewPool
        )
        {
            m_ballControllerPool = ballControllerPool;
            m_ballPresenterPool = ballPresenterPool;
            m_ballViewPool = ballViewPool;
        }

        public BallController SpawnBall(BallData ballData)
        {
            int id = ballData.Id;
            var ballView = m_ballViewPool.Spawn();
            var ballPresenter = m_ballPresenterPool.Spawn(ballView, ballData);
            var ballController = m_ballControllerPool.Spawn(ballPresenter, ballData);
            var ballContainer = new BallContainer(id, ballController, ballPresenter, ballView);
            m_balls.Add(ballContainer);
            return ballController;
        }

        public void DespawnBall(BallController ballController)
        {
            int id = ballController.Id;
            int ballIndex = FindBallContainer(id, out BallContainer ballContainer);
            // var ballContainer = FindBallContainer(id);
            m_ballViewPool.Despawn(ballContainer.View);
            m_ballPresenterPool.Despawn(ballContainer.Presenter);
            m_ballControllerPool.Despawn(ballContainer.Controller);
            
            m_balls.RemoveAt(ballIndex);
        }

        private int FindBallContainer(int id, out BallContainer ballContainer)
        {
            for (int i = 0; i < m_balls.Count; i++)
            {
                var tempBallContainer = m_balls[i];
                if (tempBallContainer.Id == id)
                {
                    ballContainer = tempBallContainer;
                    return i;
                }
            }

            ballContainer = default;
            return -1;
        }
        
        
        private struct BallContainer
        {
            public readonly int Id;
            public readonly BallController Controller;
            public readonly BallPresenter Presenter;
            public readonly BallView View;

            
            public BallContainer (int id, BallController controller, BallPresenter presenter, BallView view)
            {
                Id = id;
                Controller = controller;
                Presenter = presenter;
                View = view;
            }
        }
    }
}