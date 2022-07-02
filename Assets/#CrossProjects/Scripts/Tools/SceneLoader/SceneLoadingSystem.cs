using System;
using _CrossProjects.Tools.Coroutines;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Zenject;

namespace _CrossProjects.Tools.SceneLoader
{
    public class SceneLoadingSystem
    {
        private readonly ProjectCoroutinesProvider m_projectCoroutinesProvider = null;


        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

        
        [Inject] [UsedImplicitly]
        public SceneLoadingSystem(ProjectCoroutinesProvider projectCoroutinesProvider)
        {
            m_projectCoroutinesProvider = projectCoroutinesProvider;
        }

        public void Load(string sceneName)
        {
            var sceneLoader = new SceneLoader(sceneName);
            sceneLoader.Load();
        }

        public void Load(int sceneIndex)
        {
            var sceneLoader = new SceneLoader(sceneIndex);
            sceneLoader.Load();
        }

        public SceneLoaderAsync LoadAsync(string sceneName)
        {
            var sceneLoader = new SceneLoaderAsync(sceneName, m_projectCoroutinesProvider);
            sceneLoader.Load();
            return sceneLoader;
        }

        public SceneLoaderAsync LoadAsync(int sceneIndex)
        {
            var sceneLoader = new SceneLoaderAsync(sceneIndex, m_projectCoroutinesProvider);
            sceneLoader.Load();
            return sceneLoader;
        }
    }
}