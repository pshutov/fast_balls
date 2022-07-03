using UnityEngine;

namespace _FastBalls.Gameplay.Configs
{
    [CreateAssetMenu(fileName = ASSET_FILE_NAME, menuName = ASSET_FILE_PATH, order = ASSET_MENU_ORDER)]
    public class LevelConfig : ScriptableObject
    {
        private const string ASSET_FILE_NAME = nameof(LevelConfig);
        private const string ASSET_FILE_PATH = nameof(_FastBalls) + "/Configs/" + ASSET_FILE_NAME;
        private const int ASSET_MENU_ORDER = int.MinValue + 102;


        [SerializeField]
        private float m_levelTime = 60f;
        
        [Header("BALLS")]
        [SerializeField]
        private Vector2 m_speedRange = Vector2.right;
        [SerializeField]
        private Vector2 m_spawnDelayRange = Vector2.right;


        public float LevelTime => m_levelTime;
        
        public Vector2 SpeedRange => m_speedRange;
        public Vector2 SpawnDelayRange => m_spawnDelayRange;
    }
}