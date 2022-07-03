using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.Balls
{
    [UsedImplicitly]
    public class BallController : IPoolable<BallPresenter, BallData>
    {
        public event Action<BallController, bool> ActiveChanged = null; 


        public int Id => m_data.Id;
        
        public bool IsActive => m_data.IsActive;
        public bool IsClicked => m_data.IsClicked;
        
        public Vector3 Position => m_data.Position;
        public float Radius => m_data.Radius;
        public float Speed => m_data.Speed;
        
        public int Score => m_data.Score;
        
        
        private BallPresenter m_presenter = null;
        private BallData m_data = null;
        
        
        void IPoolable<BallPresenter, BallData>.OnSpawned(BallPresenter presenter, BallData data)
        {
            SetPresenter(presenter);
            SetData(data);
        }
        
        void IPoolable<BallPresenter, BallData>.OnDespawned()
        {
            SetData(null);
            ActiveChanged = null;
        }

        private void SetPresenter(BallPresenter presenter)
        {
            m_presenter = presenter;
        }

        private void SetData(BallData data)
        {
            if (m_data != null)
            {
                m_data.ActiveChanged -= OnActiveChanged;
            }
            
            m_data = data;

            if (data != null)
            {
                data.ActiveChanged += OnActiveChanged;
            }
        }

        public void SetPosition(Vector3 position)
        {
            m_data.SetPosition(position);
        }

        public void SetActive(bool isActive)
        {
            m_data.SetActive(isActive);
        }

        public void SetClicked(bool isClicked)
        {
            m_data.SetClicked(isClicked);
        }

        private void OnActiveChanged(bool isActive)
        {
            ActiveChanged?.Invoke(this, isActive);
        }
        
        
        [UsedImplicitly]
        public class Pool : PoolableMemoryPool<BallPresenter, BallData, BallController>
        {
            
        }
        
    }
}