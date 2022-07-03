using System;
using UnityEngine;

namespace _FastBalls.Gameplay.Balls
{
    public class BallData
    {
        public event Action<Vector3> PositionChanged = null; 
        public event Action<bool> ActiveChanged = null; 
        public event Action Clicked = null; 


        public readonly int Id = 0;
        public readonly float Radius = 0;
        public readonly Color Color = Color.white;
        
        public readonly int Score = 0;
        public readonly float Speed = 0;
        
        
        public Vector3 Position { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsClicked { get; private set; }


        public BallData(int id, float radius, Color color, int score, float speed)
        {
            Id = id;
            
            Radius = radius;
            Color = color;
            
            Score = score;
            Speed = speed;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            PositionChanged?.Invoke(position);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            ActiveChanged?.Invoke(isActive);
        }

        public void SetClicked(bool isClicked)
        {
            IsClicked = isClicked;
            Clicked?.Invoke();
        }
    }
}