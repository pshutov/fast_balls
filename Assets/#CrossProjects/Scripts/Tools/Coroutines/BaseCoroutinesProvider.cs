using UnityEngine;

namespace _CrossProjects.Tools.Coroutines
{
    public abstract class BaseCoroutinesProvider<T> : BaseCoroutinesProvider
        where T : BaseCoroutinesProvider<T>
    {
    }
    
    public abstract class BaseCoroutinesProvider : MonoBehaviour
    {
        
    }
}