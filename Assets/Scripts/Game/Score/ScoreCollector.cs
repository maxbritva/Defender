using System;

namespace Game.Score
{
    public class ScoreCollector
    {
        public  Action OnScoreChanged;
        private int _currentScore = 0;
        public int CurrentScore => _currentScore;

        public void AddScore(int value)
        {
            if (value >= 0)
            {
                _currentScore += value;
                OnScoreChanged?.Invoke();
            }
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}