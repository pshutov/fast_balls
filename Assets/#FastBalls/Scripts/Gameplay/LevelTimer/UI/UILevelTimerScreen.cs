using _CrossProjects.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _FastBalls.Gameplay.LevelTimer.UI
{
    public class UILevelTimerScreen : BaseScreen
    {
        [Header("INFO")]
        [SerializeField]
        private TMP_Text m_timerText = null;
        [SerializeField]
        private Slider m_timerSlider = null;


        public new void Show(bool animated = true)
        {
            base.Show(animated);
        }
        
        public new void Hide(bool animated = true)
        {
            base.Hide(animated);
        }
        
        public void ChangeTimeValue(float time, float maxTime)
        {
            ChangeTimeValueText(time);
            ChangeTimeValueSlider(time, maxTime);
        }

        private void ChangeTimeValueText(float time)
        {
            string text = time.ToString("F0");
            m_timerText.text = text;
        }

        private void ChangeTimeValueSlider(float time, float maxTime)
        {
            float progress = time / maxTime;
            m_timerSlider.value = progress;
        }
    }
}