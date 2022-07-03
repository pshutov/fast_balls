using System;
using _CrossProjects.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _FastBalls.Gameplay.LevelFinish.UI
{
    public class UILevelFinishScreen : BaseScreen
    {
        public event Action CloseButtonClicked = null; 


        [Header("LEVEL FINISH")]
        [Header("BUTTONS")]
        [SerializeField]
        private Button m_closeButton = null;
        
        [Header("INFO")]
        [SerializeField]
        private TMP_Text m_finishScoreText = null;


        protected override void OnEnable()
        {
            base.OnEnable();
            
            m_closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            m_closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public new void Show(bool animated = true)
        {
            base.Show(animated);
        }
        
        public new void Hide(bool animated = true)
        {
            base.Hide(animated);
        }

        public void ChangeScore(int score)
        {
            string text = score.ToString();
            m_finishScoreText.text = text;
        }

        protected override void OnStateChanged(States state)
        {
            base.OnStateChanged(state);

            switch (state)
            {
                case States.SHOWED:
                    m_closeButton.enabled = true;
                    break;
                case States.HIDING:
                    m_closeButton.enabled = false;
                    break;
            }
        }

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}