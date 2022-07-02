using UnityEngine;
using Zenject;

namespace _CrossProjects.Loading
{
    public class AppLoadingMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<AppLoadingController>().FromNew().AsSingle().NonLazy();
        }
    }
}