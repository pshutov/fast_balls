using System;
using UnityEngine;

namespace _FastBalls.Gameplay.Balls.Configs
{
    [CreateAssetMenu(fileName = ASSET_FILE_NAME, menuName = ASSET_FILE_PATH, order = ASSET_MENU_ORDER)]
    public class BallsConfig : ScriptableObject
    {
        private const string ASSET_FILE_NAME = nameof(BallsConfig);
        private const string ASSET_FILE_PATH = nameof(_FastBalls) + "/Configs/" + ASSET_FILE_NAME;
        private const int ASSET_MENU_ORDER = int.MinValue + 121;


        [Header("BY DIFFICULT")]
        [SerializeField]
        private AnimationCurve m_radiusRange = AnimationCurve.Linear(0f, 1f, 1f, 0.5f);
        [SerializeField]
        private AnimationCurve m_speedFactorRange = AnimationCurve.Linear(0f, 1f, 1f, 2f);
        [SerializeField]
        private Vector2Int m_scoreRange = Vector2Int.right;
        [SerializeField]
        private Gradient m_colorGradient = null;


        public AnimationCurve RadiusRange => m_radiusRange;
        public AnimationCurve SpeedFactorRange => m_speedFactorRange;
        public Vector2Int ScoreRange => m_scoreRange;
        public Gradient ColorGradient => m_colorGradient;
    }
}