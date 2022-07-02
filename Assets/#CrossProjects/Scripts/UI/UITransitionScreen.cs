using System;
using _CrossProjects.Common.UI;

namespace _CrossProjects.UI
{
    public class UITransitionScreen : BaseScreen
    {
        private Action m_showCallback = null;
        

        public new void Show(bool animated, Action showedCallback = null)
        {
            m_showCallback = showedCallback;
            base.Show(animated);
        }

        public new void Hide(bool animated, Action callback = null)
        {
            base.Hide(animated);
        }

        protected override void OnStateChanged(States state)
        {
            base.OnStateChanged(state);

            switch (state)
            {
                case States.SHOWED:
                    m_showCallback?.Invoke();
                    break;
            }
        }
    }
}