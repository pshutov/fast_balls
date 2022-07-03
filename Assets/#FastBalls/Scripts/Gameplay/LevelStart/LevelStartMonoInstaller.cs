using _FastBalls.Gameplay.LevelStart.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.LevelStart
{
    public sealed class LevelStartMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private UILevelStartScreen m_levelStartScreen = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelStartSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelStartPresenter>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<UILevelStartScreen>()
                .FromComponentInNewPrefab(m_levelStartScreen)
                .UnderTransformGroup("UICanvas")
                .AsSingle();
        }
    }
}