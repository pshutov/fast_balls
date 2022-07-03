using _CrossProjects.Common.UI;
using TMPro;
using UnityEngine;

namespace _FastBalls.Gameplay.LevelScore.UI
{
    public class UILevelScoreScreen : BaseScreen
    {
        [Header("LEVEL SCORE")]
        [Header("INFO")]
        [SerializeField]
        private TMP_Text m_scoreText = null;
        
        
        public new void Show(bool animated = true)
        {
            base.Show(animated);
        }
        
        public new void Hide(bool animated = true)
        {
            base.Hide(animated);
        }

        public void ChangeScore(int value)
        {
            m_scoreText.text = value.ToString();
        }
    }
}