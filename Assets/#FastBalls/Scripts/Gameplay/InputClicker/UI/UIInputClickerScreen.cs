using System;
using _CrossProjects.Common.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _FastBalls.Gameplay.InputClicker.UI
{
    public class UIInputClickerScreen : BaseScreen, IPointerClickHandler
    {
        public event Action<Vector3> Clicked = null; 


        public new void Show(bool animated = true)
        {
            base.Show(animated);
        }
        
        public new void Hide(bool animated = true)
        {
            base.Hide(animated);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            var eventCamera = eventData.pressEventCamera;
            var worldPosition = eventCamera.ScreenToWorldPoint(eventData.position);
            Clicked?.Invoke(worldPosition);
        }
    }
}