using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenu.Shop
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balancePlayerData;
        private PlayerData _playerData;

        private void Start() => UpdateValue( _playerData.Balance);

        private void OnEnable() => _playerData.BalanceChanged += UpdateValue;
        private void OnDisable() => _playerData.BalanceChanged -= UpdateValue;

        private void UpdateValue(int coins) => _balancePlayerData.text = $"АСТРОБАКСОВ: {coins}";
        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}