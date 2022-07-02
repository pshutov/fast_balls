using System;
using UnityEngine;

namespace _CrossProjects.Tools
{
    public class AnimationCallbacksHandler : MonoBehaviour
    {
        public event Action<string> StringInvoked = null;
        public event Action<float> FloatInvoked = null;
        public event Action<float> IntInvoked = null;
        

        private void InvokeString(string value)
        {
            StringInvoked?.Invoke(value);
        }
        
        private void InvokeFloat(float value)
        {
            FloatInvoked?.Invoke(value);
        }
        
        private void InvokeInt(int value)
        {
            IntInvoked?.Invoke(value);
        }
    }
}