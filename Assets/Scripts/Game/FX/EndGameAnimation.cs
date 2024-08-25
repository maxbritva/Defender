using System;
using System.Collections;
using Game.GameCore.GameStates;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    [RequireComponent(typeof(AudioSource))]
    public class EndGameAnimation : MonoBehaviour
    {
        public Action OnAnimationEnd;
        [SerializeField] private TMP_Text _textCounter;
        [SerializeField] private AudioSource _audioSource;
        private EndGame _endGame;
        private WaitForSeconds _tick = new WaitForSeconds(0.1f);
        private int _currentValue;
        private int _targetValue;
        private float _durationAnimation;
        private float _soundFXTimerTick;
        private float _animateRate;

        public void StartAnimation()
        {
            _durationAnimation = _endGame.BalanceToAdd < 10 ? 1f : 3f;
            StartCoroutine(Animate(_endGame.BalanceToAdd, 0, _textCounter));
        }

        private IEnumerator Animate(float targetValue, float currentValue, TMP_Text targetText)
        {
            StartCoroutine(SoundFX());
           _animateRate = Mathf.Abs(targetValue - currentValue) / _durationAnimation;
            while (currentValue < targetValue) {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, _animateRate * Time.deltaTime);
             _textCounter.text = currentValue.ToString();
                yield return null;
            }
            OnAnimationEnd?.Invoke();
        }
        
        private IEnumerator SoundFX() {
            _soundFXTimerTick = 0f;
            _audioSource.pitch = 1f;
            while (_soundFXTimerTick <=_durationAnimation) {
                _audioSource.Play();
                _audioSource.pitch += 0.1f;
                _soundFXTimerTick += 0.1f;
                yield return _tick;
            }
        }

        [Inject] private void Construct(EndGame endGame) => _endGame = endGame;
    }
}