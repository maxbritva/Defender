using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameCore.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.FX
{
    public class GameStartAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _GameUIPanel;
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite[] _allSprites;
        [SerializeField] private Vector3 _scaleTo;
        private CancellationTokenSource _cancellationToken;
        private float _duration = 1f;
        
        private GameManager _gameManager;
        
        public async void StartAnimation() 
        {
            _cancellationToken = new CancellationTokenSource();
            _targetImage.gameObject.SetActive(true);
            await PrepareForBattleAnimation();
        }

        private async UniTask PrepareForBattleAnimation()
        {
            for (int i = 0; i < 4; i++) {
                _targetImage.sprite = _allSprites[i];
                ScaleImageAnimation();
                await UniTask.Delay(TimeSpan.FromSeconds(1f), _cancellationToken.IsCancellationRequested);
            }
            _GameUIPanel.SetActive(true);
            _targetImage.gameObject.SetActive(false);
            _gameManager.OnGameStarted?.Invoke();
            _cancellationToken.Cancel();
        }
        private void ScaleImageAnimation() => _targetImage.transform.DOScale(_scaleTo, _duration).SetLoops(-1, LoopType.Restart);

        [Inject] private void Construct(GameManager gameManager) => _gameManager = gameManager;
    }
}