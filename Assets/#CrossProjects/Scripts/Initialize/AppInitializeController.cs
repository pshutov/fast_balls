using _CrossProjects.Tools.SceneLoader;
using Zenject;

namespace _CrossProjects.Initialize
{
    public class AppInitializeController : IInitializable
    {
        private readonly SceneLoadingSystem m_sceneLoadingSystem = null;
        
        
        [Inject]
        public AppInitializeController(SceneLoadingSystem sceneLoadingSystem)
        {
            m_sceneLoadingSystem = sceneLoadingSystem;
        }

        void IInitializable.Initialize()
        {
            int nextScene = m_sceneLoadingSystem.CurrentSceneIndex + 1;
            m_sceneLoadingSystem.Load(nextScene);
        }
    }
}