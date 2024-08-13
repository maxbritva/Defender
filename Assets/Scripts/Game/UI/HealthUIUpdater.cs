using Game.Health;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class HealthUIUpdater: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        private PlayerHealth _playerHealth;

        private void OnEnable()
        {
            _playerHealth.OnPlayerHit += UpdateHealthText;
            _playerHealth.OnPlayerHeal += UpdateHealthText;
        }
       
        private void OnDisable()
        {
            _playerHealth.OnPlayerHit -= UpdateHealthText;
            _playerHealth.OnPlayerHeal -= UpdateHealthText;
        }
        private void Start() => UpdateHealthText();
        private void UpdateHealthText() => _healthText.text = _playerHealth.CurrentHealth.ToString();

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}