using _CrossProjects.Tools.Coroutines;
using Zenject;

namespace _CrossProjects.Common
{
    public class SceneMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTools();
        }

        private void BindTools()
        {
            Container.Bind<SceneCoroutinesProvider>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}