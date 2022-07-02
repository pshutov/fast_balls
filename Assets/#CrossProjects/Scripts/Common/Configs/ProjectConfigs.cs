using UnityEngine;

namespace _CrossProjects.Common.Configs
{
    [CreateAssetMenu(fileName = ASSET_FILE_NAME, menuName = ASSET_FILE_PATH, order = ASSET_MENU_ORDER)]
    public class ProjectConfigs : ScriptableObject
    {
        private const string ASSET_FILE_NAME = nameof(ProjectConfigs);
        private const string ASSET_FILE_PATH = nameof(_CrossProjects) + "/Configs/" + ASSET_FILE_NAME;
        private const int ASSET_MENU_ORDER = int.MinValue + 1;


        [Header("QUALITY")] 
        [SerializeField]
        private int m_targetFps = 60;
        
        [Header("INPUT")]
        [SerializeField] 
        private bool m_multitouchInput = false;
        [SerializeField] 
        private bool m_simulateMouseWithTouches = false;


        public int TargetFps => m_targetFps;
        
        public bool MultitouchInput => m_multitouchInput;
        public bool SimulateMouseWithTouches => m_simulateMouseWithTouches;
    }
}
