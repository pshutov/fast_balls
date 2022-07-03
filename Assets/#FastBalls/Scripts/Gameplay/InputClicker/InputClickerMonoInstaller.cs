using _FastBalls.Gameplay.InputClicker.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.InputClicker
{
    public class InputClickerMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private UIInputClickerScreen m_inputClickerScreen = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputClickerSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<InputClickerPresenter>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<UIInputClickerScreen>()
                .FromComponentInNewPrefab(m_inputClickerScreen)
                .UnderTransformGroup("UICanvas")
                .AsSingle();
        }
    }
}