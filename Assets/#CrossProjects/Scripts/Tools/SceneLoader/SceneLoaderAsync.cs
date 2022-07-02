using System;
using System.Collections;
using _CrossProjects.Tools.Coroutines;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace _CrossProjects.Tools.SceneLoader
{
    public class SceneLoaderAsync : ISceneLoader
    {
        public event Action<SceneLoaderAsync> LoadingCompleted = null; 
        
        public event Action<float> LoadingProgressChanged = null; 


        private readonly BaseCoroutinesProvider m_coroutinesProvider = null;
        
        
        private int m_sceneIndex = 0;
        private Coroutine m_loadingCoroutine = null;
        
        
        public bool IsAllowSceneActivation { get; private set; }
        public bool IsLoaded { get; private set; }
        
        
        public int SceneIndex => m_sceneIndex;
        public bool IsLoading => m_loadingCoroutine != null;


        private SceneLoaderAsync(BaseCoroutinesProvider coroutinesProvider)
        {
            m_coroutinesProvider = coroutinesProvider;
        }

        public SceneLoaderAsync(int sceneIndex, BaseCoroutinesProvider coroutinesProvider)
            : this(coroutinesProvider)
        {
            m_sceneIndex = sceneIndex;
        }

        public SceneLoaderAsync(string sceneName, BaseCoroutinesProvider coroutinesProvider)
            : this(coroutinesProvider)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            m_sceneIndex = scene.buildIndex;
        }

        public void Load()
        {
            LoadAsync(SceneIndex, LoadingComplete);
        }

        public void SetAllowSceneActivation(bool value)
        {
            IsAllowSceneActivation = value;
        }

        private void LoadingComplete()
        {
            IsLoaded = true;
            LoadingCompleted?.Invoke(this);
        }

        private void LoadAsync(int sceneIndex, Action callback)
        {
            Assert.IsFalse(IsLoading);
            
            var loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
            m_loadingCoroutine = m_coroutinesProvider.StartCoroutine(LoadingCoroutine(loadOperation, OnComplete));

            void OnComplete()
            {
                m_loadingCoroutine = null;
                
                callback?.Invoke();
            }
        }
        
        private IEnumerator LoadingCoroutine(AsyncOperation loadOperation, Action callback)
        {
            loadOperation.allowSceneActivation = false;
            
            while (!loadOperation.isDone && loadOperation.progress < 0.9f)
            {
                LoadingProgressChanged?.Invoke(loadOperation.progress);
                yield return null;
            }

            yield return new WaitUntil(() => IsAllowSceneActivation);

            loadOperation.allowSceneActivation = true;
            
            while (!loadOperation.isDone)
            {
                LoadingProgressChanged?.Invoke(loadOperation.progress);
                yield return null;
            }
            
            LoadingProgressChanged?.Invoke(loadOperation.progress);
            
            callback?.Invoke();
        }
    }
}