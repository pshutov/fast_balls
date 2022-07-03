using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    [UsedImplicitly]
    public class BallPresenter : IPoolable<BallView, BallData>
    {
        private BallView m_view = null;
        private BallData m_data = null;
        
        
        void IPoolable<BallView, BallData>.OnSpawned(BallView view, BallData data)
        {
            SetView(view);
            SetData(data);
            
            UpdateView();
        }
        
        void IPoolable<BallView, BallData>.OnDespawned()
        {
            SetView(null);
            SetData(null);
        }

        private void SetView(BallView view)
        {
            m_view = view;
        }

        private void SetData(BallData data)
        {
            if (m_data != null)
            {
                m_data.PositionChanged -= OnPositionChanged;
                m_data.ActiveChanged -= OnActiveChanged;
            }
            
            m_data = data;

            if (data != null)
            {
                data.PositionChanged += OnPositionChanged;
                data.ActiveChanged += OnActiveChanged;
            }
        }

        private void UpdateView()
        {
            int id = m_data.Id;
            m_view.ChangeSortingOrder(id);
            float radius = m_data.Radius;
            m_view.ChangeRadius(radius);
            var color = m_data.Color;
            m_view.ChangeColor(color);
            bool isActive = m_data.IsActive;
            m_view.ChangeVisible(isActive);

            var position = m_data.Position;
            m_view.ChangePosition(position);
        }

        private void OnActiveChanged(bool isActive)
        {
            m_view.ChangeVisible(isActive);
        }

        private void OnPositionChanged(Vector3 position)
        {
            m_view.ChangePosition(position);
        }
        
        
        [UsedImplicitly]
        public class Pool : PoolableMemoryPool<BallView, BallData, BallPresenter>
        {
            
        }
        
    }
}