using System;
using UnityEngine.SceneManagement;

namespace _CrossProjects.Tools.SceneLoader
{
    public class SceneLoader: ISceneLoader
    {
        private int m_sceneIndex = 0;
        
        
        public int SceneIndex => m_sceneIndex;


        public SceneLoader(int sceneIndex)
        {
            m_sceneIndex = sceneIndex;
        }
        
        public SceneLoader(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            m_sceneIndex = scene.buildIndex;
        }

        public void Load()
        {
            Load(SceneIndex);
        }

        private void Load(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}