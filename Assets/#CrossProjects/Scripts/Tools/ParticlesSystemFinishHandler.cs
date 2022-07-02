using System;
using UnityEngine;

namespace _CrossProjects.Tools
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlesSystemFinishHandler : MonoBehaviour
    {
        public event Action Stopped = null; 


        private ParticleSystem m_particleSystem = null;
        
        
        private void Awake()
        {
            m_particleSystem = GetComponent<ParticleSystem>();
            
            var main = m_particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }    
        
        public void OnParticleSystemStopped()
        {
            Stopped?.Invoke();
        }
    }
}