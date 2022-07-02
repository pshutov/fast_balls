using _CrossProjects.Common.Configs;
using UnityEngine;
using Zenject;

namespace _CrossProjects.Common
{
    public class ProjectController : IInitializable
    {
        private readonly ProjectConfigs m_projectConfigs = null;
        
        
        [Inject]
        public ProjectController(ProjectConfigs projectConfigs)
        {
            m_projectConfigs = projectConfigs;
        }

        void IInitializable.Initialize()
        {
            SetTargetFpsConfigs(m_projectConfigs);
            SetInputConfigs(m_projectConfigs);
        }

        private void SetTargetFpsConfigs(ProjectConfigs projectConfigs)
        {
            Application.targetFrameRate = projectConfigs.TargetFps;
        }

        private void SetInputConfigs(ProjectConfigs projectConfigs)
        {
            Input.multiTouchEnabled = projectConfigs.MultitouchInput;
            Input.simulateMouseWithTouches = projectConfigs.SimulateMouseWithTouches;
        }
    }
}