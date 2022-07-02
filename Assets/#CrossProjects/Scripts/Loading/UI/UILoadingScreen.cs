using _CrossProjects.Tools.SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _CrossProjects.Loading.UI
{
    public class UILoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Slider m_progressSlider = null;
        [SerializeField]
        private TMP_Text m_progressText = null;
        
        
        private AppLoadingController m_appLoadingController = null;
        
        
        [Inject]
        private void Construct(
            AppLoadingController appLoadingController
        )
        {
            m_appLoadingController = appLoadingController;
        }

        private void OnEnable()
        {
            m_appLoadingController.SceneLoaderChanged += OnSceneLoaderChanged;
            
            SetLoadingProgress(0f);
        }

        private void OnDisable()
        {
            m_appLoadingController.SceneLoaderChanged -= OnSceneLoaderChanged;
        }

        private void OnSceneLoaderChanged(SceneLoaderAsync sceneLoader)
        {
            sceneLoader.LoadingProgressChanged -= OnLoadingProgressChanged;
            sceneLoader.LoadingProgressChanged += OnLoadingProgressChanged;
        }

        private void OnLoadingProgressChanged(float value)
        {
            SetLoadingProgress(value);
        }

        private void SetLoadingProgress(float value)
        {
            if (m_progressText != null)
            {
                string text = value.ToString("P0");
                m_progressText.text = text;
            }

            if (m_progressSlider != null)
            {
                m_progressSlider.value = value;
            }
        }
    }
}