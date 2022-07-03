using _FastBalls.Gameplay.LevelFinish.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.LevelFinish
{
    public sealed class LevelFinishMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private UILevelFinishScreen m_levelFinishScreenPrefab = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelFinishSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelFinishPresenter>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<UILevelFinishScreen>()
                .FromComponentInNewPrefab(m_levelFinishScreenPrefab)
                .UnderTransformGroup("UICanvas")
                .AsSingle();
        }
    }
}