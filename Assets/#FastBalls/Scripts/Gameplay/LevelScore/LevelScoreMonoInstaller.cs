using _FastBalls.Gameplay.LevelScore.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.LevelScore
{
    public sealed class LevelScoreMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private UILevelScoreScreen m_levelScoreScreenPrefab = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelScoreSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelScorePresenter>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<UILevelScoreScreen>()
                .FromComponentInNewPrefab(m_levelScoreScreenPrefab)
                .UnderTransformGroup("UICanvas")
                .AsSingle();
        }
    }
}