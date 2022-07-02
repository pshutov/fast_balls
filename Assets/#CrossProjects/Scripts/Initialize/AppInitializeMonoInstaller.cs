using Zenject;

namespace _CrossProjects.Initialize
{
    public class AppInitializeMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<AppInitializeController>().FromNew().AsSingle().NonLazy();
        }
    }
}