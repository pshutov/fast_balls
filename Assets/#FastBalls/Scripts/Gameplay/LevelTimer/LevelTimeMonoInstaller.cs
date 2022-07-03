using _FastBalls.Gameplay.LevelTimer.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.LevelTimer
{
    public sealed class LevelTimeMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private UILevelTimerScreen m_levelTimerScreen = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelTimerSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelTimerPresenter>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<UILevelTimerScreen>()
                .FromComponentInNewPrefab(m_levelTimerScreen)
                .UnderTransformGroup("UICanvas")
                .AsSingle();
        }
    }
}