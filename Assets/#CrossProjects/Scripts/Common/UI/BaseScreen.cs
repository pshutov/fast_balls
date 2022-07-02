using System;
using _CrossProjects.Tools;
using UnityEngine;

namespace _CrossProjects.Common.UI
{
    public class BaseScreen : MonoBehaviour
    {
        public event Action<States> StateChanged = null; 
        
        
        [Header("OBJECT")]
        [SerializeField]
        private GameObject m_gameObject = null;
        
        [Header("UI")]
        [SerializeField]
        private Canvas m_canvas = null;

        [Header("ANIMATIONS")]
        [SerializeField]
        private Animator m_animator = null;
        [SerializeField]
        private AnimationCallbacksHandler m_animationCallbacksHandler = null;


        private bool m_destroing = false;
        
        
        public States State { get; private set; } = States.NONE;
        
        
        protected void Awake()
        {
            if (State == States.NONE)
            {
                Hide(false);
                SetState(States.HIDDEN);
            }
        }

        protected void OnDestroy()
        {
            m_destroing = true;
        }

        protected virtual void OnEnable()
        {
            m_animationCallbacksHandler.StringInvoked += OnScreenAnimatorCallbacks;
        }

        protected virtual void OnDisable()
        {
            m_animationCallbacksHandler.StringInvoked -= OnScreenAnimatorCallbacks;
        }

        protected void Show(bool animated)
        {
            m_canvas.enabled = true;
            m_gameObject.SetActive(true);
            
            SetState(States.SHOWING);
            
            m_animator.enabled = true;
            
            if (!animated)
            {
                m_animator.Play(AnimationsProperties.Show, -1, 1);
            }
            else
            {
                m_animator.Play(AnimationsProperties.Show, -1);
            }
        }

        protected void Hide(bool animated)
        {
            if (m_destroing)
            {
                return;
            }
            
            SetState(States.HIDING);
            
            m_animator.enabled = true;
            
            if (!animated)
            {
                m_animator.Play(AnimationsProperties.Hide, -1, 1);
            }
            else
            {
                m_animator.Play(AnimationsProperties.Hide, -1);
            }
        }

        protected virtual void OnScreenAnimatorCallbacks(string data)
        {
            switch (data)
            {
                case AnimationsProperties.finish:
                    m_animator.enabled = false;

                    switch (State)
                    {
                        case States.SHOWING:
                            SetState(States.SHOWED);
                            break;
                        case States.HIDING:
                            SetState(States.HIDDEN);
                            break;
                    }
                    break;
            }
        }

        private void SetState(States state)
        {
            State = state;
            OnStateChanged(state);
            StateChanged?.Invoke(state);
        }

        protected virtual void OnStateChanged(States state)
        {
            switch (state)
            {
                case States.HIDDEN:
                    m_canvas.enabled = false;
                    m_gameObject.SetActive(false);
                    break;
            }
        }


        protected class AnimationsProperties
        {
            public const string finish = "finish";
            
            public static readonly int Show = Animator.StringToHash("Show");
            public static readonly int Hide = Animator.StringToHash("Hide");
        }
        
        public enum States
        {
            NONE = 0,
            
            SHOWING,
            SHOWED,
            
            HIDING,
            HIDDEN
        }
    }
}