using _FastBalls.Gameplay.Balls.Configs;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public sealed class BallsMonoInstaller : MonoInstaller
    {
        [Header("PREFABS")]
        [SerializeField]
        private BallView m_ballViewPrefab = null;
        
        [Header("CONFIGS")]
        [SerializeField]
        private BallsConfig m_ballsConfig = null;
        [SerializeField]
        private MemoryPoolSettings m_poolSettings = MemoryPoolSettings.Default;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BallsSpawnerSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BallsCreatorSystem>().FromNew().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<BallsOutsideCheckerSystem>().FromNew().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<BallsClickerSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BallsScoresSystem>().FromNew().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<BallsMovementSystem>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<BallsContainer>().FromNew().AsSingle().NonLazy();
            
            Container.BindInstance(m_ballsConfig).AsSingle().NonLazy();
            
            BindBallsPools(m_ballViewPrefab);
        }

        private void BindBallsPools(BallView prefab)
        {
            Container.BindMemoryPool<BallController, BallController.Pool>()
                .WithInitialSize(m_poolSettings.InitialSize)
                .WithMaxSize(m_poolSettings.MaxSize)
                .FromNew();
            
            Container.BindMemoryPool<BallPresenter, BallPresenter.Pool>()
                .WithInitialSize(m_poolSettings.InitialSize)
                .WithMaxSize(m_poolSettings.MaxSize)
                .FromNew();
            
            Container.BindMemoryPool<BallView, BallView.Pool>()
                .WithInitialSize(m_poolSettings.InitialSize)
                .WithMaxSize(m_poolSettings.MaxSize)
                .FromComponentInNewPrefab(prefab)
                .UnderTransformGroup("BALLS");
        }
    }
}