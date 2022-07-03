using System;
using _FastBalls.Gameplay.InputClicker.UI;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay.InputClicker
{
    public class InputClickerPresenter
    {
        public event Action<Vector3> Clicked = null; 


        private readonly UIInputClickerScreen m_screen = null;
        
        
        [Inject]
        public InputClickerPresenter(
            UIInputClickerScreen screen
        )
        {
            m_screen = screen;
        }

        public void ShowScreen()
        {
            m_screen.Show(true);
            m_screen.Clicked += OnClicked;
        }

        public void HideScreen()
        {
            m_screen.Hide(true);
            m_screen.Clicked -= OnClicked;
        }

        private void OnClicked(Vector3 worldPosition)
        {
            Clicked?.Invoke(worldPosition);
        }
    }
}