using DG.Tweening;
using Game.Score;
using Game.UI;
using Player;
using Save;
using Zenject;

namespace Game.GameCore.GameStates.EndGame
{
    public class EndGameManager
    {
        private ScoreCollector _scoreCollector;
        private PlayerData _playerData;
        private DataProvider _dataProvider;
        private EndGameUI _endGameUI;
        private int _balanceToAdd = 0;
        public int BalanceToAdd => _balanceToAdd;

        public void Initialize()
        {
            CalculateBalanceToAdd();
            CheckTopScores();
            DOTween.KillAll();
            _dataProvider.Save();
        }
        private void CalculateBalanceToAdd()
        {
            if (_scoreCollector.CurrentScore <= 0) return;
            _balanceToAdd = _scoreCollector.CurrentScore / 8;
            if (_balanceToAdd < 1)
                _balanceToAdd = 1;
            _playerData.AddBalance(_balanceToAdd);
        }
       

        private void CheckTopScores()
        {
            if (_scoreCollector.CurrentScore <= _playerData.TopScore) return;
            _playerData.SetTopScore(_scoreCollector.CurrentScore);
            _endGameUI.ShowTopScore();
        }

        [Inject] private void Construct(ScoreCollector scoreCollector, PlayerData playerData, EndGameUI endGameUI, DataProvider dataProvider)
        {
            _scoreCollector = scoreCollector;
            _playerData = playerData;
            _dataProvider = dataProvider;
            _endGameUI = endGameUI;
        }
    }
}