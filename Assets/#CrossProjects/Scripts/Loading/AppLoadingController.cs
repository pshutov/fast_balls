using System;
using _CrossProjects.Tools.SceneLoader;
using Zenject;

namespace _CrossProjects.Loading
{
    public class AppLoadingController : IInitializable, IDisposable
    {
        public event Action<SceneLoaderAsync> SceneLoaderChanged = null;


        private readonly SceneLoadingSystem m_sceneLoadingSystem = null;


        private SceneLoaderAsync m_sceneLoader = null;
        
        
        [Inject]
        public AppLoadingController(
            SceneLoadingSystem sceneLoadingSystem
        )
        {
            m_sceneLoadingSystem = sceneLoadingSystem;
        }

        void IInitializable.Initialize()
        {
            int nextScene = m_sceneLoadingSystem.CurrentSceneIndex + 1;
            
            m_sceneLoader = m_sceneLoadingSystem.LoadAsync(nextScene);
            m_sceneLoader.SetAllowSceneActivation(false);
            
            SceneLoaderChanged?.Invoke(m_sceneLoader);
            
            //TODO Add waiting for analytics
            OnInitialized();
        }
		
        void IDisposable.Dispose()
        {
        }

        private void OnInitialized()
        {
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            m_sceneLoader.SetAllowSceneActivation(true);
        }
    }
}