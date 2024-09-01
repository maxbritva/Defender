using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameCore.GameStates.EndGame;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    [RequireComponent(typeof(AudioSource))]
    public class EndGameAnimation : MonoBehaviour
    {
        public event Action OnAnimationEnd;
        
        [SerializeField] private TMP_Text _textCounter;
        [SerializeField] private AudioSource _audioSource;
        private EndGameManager _endGameManager;
        private CancellationTokenSource _cancellationToken;
        private int _currentValue;
        private int _targetValue;
        private float _durationAnimation;
        private float _soundFXTimerTick;
        private float _animateRate;

        public async UniTask StartAnimation()
        {
            _cancellationToken = new CancellationTokenSource();
            _durationAnimation = _endGameManager.BalanceToAdd < 10 ? 1f : 2.5f;
            await Animate(_endGameManager.BalanceToAdd,0);
        }

        private async UniTask Animate(float targetValue, float currentValue)
        {
            _animateRate = Mathf.Abs(targetValue - currentValue) / _durationAnimation;
            _audioSource.Play();
            _audioSource.pitch = 1f;
            _audioSource.DOPitch(1.5f, _durationAnimation);
            while (currentValue < targetValue) {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, _animateRate * Time.deltaTime);
                _textCounter.text = Mathf.FloorToInt(currentValue).ToString();
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken.Token);
            }
            _cancellationToken.Cancel();
            _audioSource.Stop();
            OnAnimationEnd?.Invoke();
        }
        [Inject] private void Construct(EndGameManager endGameManager) => _endGameManager = endGameManager;
    }
}