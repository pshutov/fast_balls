using System;

namespace _FastBalls.Gameplay.LevelScore
{
    public class LevelScoreData
    {
        public event Action<int> ScoreChanged = null; 


        public int Score { get; private set; }


        public void IncScore(int deltaScore)
        {
            int score = Score + deltaScore;
            SetScore(score);
        }

        public void SetScore(int score)
        {
            Score = score;
            ScoreChanged?.Invoke(score);
        }
    }
}