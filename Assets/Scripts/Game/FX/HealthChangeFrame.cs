using System.Collections;
using Game.Health;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.FX
{
    public class HealthChangeFrame : MonoBehaviour
    {
        [SerializeField] private Color _damageColor;
        [SerializeField] private Color _healColor;
        [SerializeField] private Image _frame;
        private Coroutine _frameCoroutine;
        private PlayerHealth _playerHealth;
        private WaitForSeconds _wait;
        private float _timer;

        private void Awake() => _wait = new WaitForSeconds(0.05f);

        private void OnEnable()
        {
            _playerHealth.OnPlayerHit += DamageFrame;
            _playerHealth.OnPlayerHeal += HealFrame;
        }

        private void OnDisable()
        {
            _playerHealth.OnPlayerHit -= DamageFrame;
            _playerHealth.OnPlayerHeal -= HealFrame;
            if(_frameCoroutine != null)
                StopCoroutine(_frameCoroutine);
        }

        private void HealFrame() => _frameCoroutine = StartCoroutine(  ShowFrame(_healColor));
        private void DamageFrame() => _frameCoroutine = StartCoroutine(  ShowFrame(_damageColor));

        private IEnumerator ShowFrame(Color target) {
            _frame.color = target;
            var color = _frame.color;
            color.a = 0.5f;
            for (float f = 0.5f; f >= -0.05f; f-=0.05f) {
                color.a = f;
                _frame.color = color;
                yield return _wait;
            }
        }

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}