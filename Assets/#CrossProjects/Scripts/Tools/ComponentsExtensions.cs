using UnityEngine;

namespace _CrowdCrasher.Tools
{
    public static class ComponentsExtensions
    {
        public static bool TryGetComponentInParent<T>(this Component @this, out T component)
        {
            component = @this.GetComponentInParent<T>();
            return component != null;
        }

        public static bool TryGetComponent<T>(this Component @this, out T component, bool checkInParent = true)
        {
            bool hasComponent = @this.TryGetComponent<T>(out component);
            if (hasComponent || !checkInParent)
            {
                return hasComponent;
            }
            
            return @this.TryGetComponentInParent<T>(out component);
        }
        
        public static bool TryGetComponentInParent<T>(this GameObject @this, out T component)
        {
            component = @this.GetComponentInParent<T>();
            return component != null;
        }

        public static bool TryGetComponent<T>(this GameObject @this, out T component, bool checkInParent = true)
        {
            bool hasComponent = @this.TryGetComponent<T>(out component);
            if (hasComponent || !checkInParent)
            {
                return hasComponent;
            }
            
            return @this.TryGetComponentInParent<T>(out component);
        }
    }
}