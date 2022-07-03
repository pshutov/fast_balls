using System;
using _CrossProjects.Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _FastBalls.Gameplay.LevelStart.UI
{
    public class UILevelStartScreen : BaseScreen
    {
        public event Action StartButtonClicked = null; 


        [Header("LEVEL START")]
        [Header("BUTTONS")]
        [SerializeField]
        private Button m_startButton = null;


        protected override void OnEnable()
        {
            base.OnEnable();
            
            m_startButton.onClick.AddListener(OnStartButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            m_startButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        public new void Show(bool animated = true)
        {
            base.Show(animated);
        }
        
        public new void Hide(bool animated = true)
        {
            base.Hide(animated);
        }

        protected override void OnStateChanged(States state)
        {
            base.OnStateChanged(state);

            switch (state)
            {
                case States.SHOWED:
                    m_startButton.enabled = true;
                    break;
                case States.HIDING:
                    m_startButton.enabled = false;
                    break;
            }
        }

        private void OnStartButtonClicked()
        {
            StartButtonClicked?.Invoke();
        }
    }
}