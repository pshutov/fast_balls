using _CrossProjects.Common.Configs;
using _CrossProjects.Tools.Coroutines;
using _CrossProjects.Tools.SceneLoader;
using UnityEngine;
using Zenject;

namespace _CrossProjects.Common
{
    public class ProjectMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private ProjectConfigs m_projectConfigs = null;
        
        
        public override void InstallBindings()
        {
            BindControllers();
            BindConfigs();
            BindTools();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<ProjectController>().FromNew().AsSingle();
        }

        private void BindTools()
        {
            Container.Bind<ProjectCoroutinesProvider>().FromNewComponentOnNewGameObject().AsSingle();
            
            Container.Bind<SceneLoadingSystem>().FromNew().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(m_projectConfigs).AsSingle();
        }
    }
}