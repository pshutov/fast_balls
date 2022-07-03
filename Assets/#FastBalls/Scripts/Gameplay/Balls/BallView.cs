using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    public class BallView : MonoBehaviour, IPoolable
    {
        [Header("OBJECT")]
        [SerializeField]
        private GameObject m_gameObject = null;
        [SerializeField]
        private Transform m_transform = null;

        [Header("VISUAL")]
        [SerializeField]
        private SpriteRenderer m_ballSpriteRenderer = null;
        [SerializeField]
        private SortingGroup m_sortingGroup = null;

        
        void IPoolable.OnSpawned()
        {
        }
        
        void IPoolable.OnDespawned()
        {
        }
        
        public void ChangeVisible(bool isVisible)
        {
            m_gameObject.SetActive(isVisible);
        }

        public void ChangeSortingOrder(int order)
        {
            m_sortingGroup.sortingOrder = order;
        }

        public void ChangeColor(Color color)
        {
            m_ballSpriteRenderer.color = color;
        }

        public void ChangeRadius(float radios)
        {
            m_transform.localScale = Vector3.one * (radios * 2f);
        }

        public void ChangePosition(Vector3 position)
        {
            m_transform.position = position;
        }
        
        
        [UsedImplicitly]
        public class Pool : MonoPoolableMemoryPool<BallView>
        {
            
        }
    }
}