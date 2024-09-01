using DG.Tweening;
using Game.Health;
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
        private float _timer;
        
        private PlayerHealth _playerHealth;

        private void OnEnable()
        {
            _playerHealth.OnPlayerHit += DamageFrame;
            _playerHealth.OnPlayerHeal += HealFrame;
        }

        private void OnDisable()
        {
            _playerHealth.OnPlayerHit -= DamageFrame;
            _playerHealth.OnPlayerHeal -= HealFrame;
        }

        private void HealFrame() => StartFrameAnimation(_healColor);
        private void DamageFrame() => StartFrameAnimation(_damageColor);

        private void StartFrameAnimation(Color target)
        {
            _frame.color = target;
            _frame.DOFade(0f, 0.5f);
        }

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}