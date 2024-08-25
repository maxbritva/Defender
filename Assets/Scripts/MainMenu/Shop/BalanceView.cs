using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MainMenu.Shop
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balancePlayerData;
        private IPlayerData _playerData;

        private void Start()
        {
            Debug.Log( _playerData.GetBalance());
            UpdateValue( _playerData.GetBalance());
        }

        private void OnEnable()
        {
           // _playerData.BalanceChanged += UpdateValue;
           // UpdateValue(_playerData.Balance);
           
        }
       // private void OnDisable() => _playerData.BalanceChanged -= UpdateValue;

        public void UpdateValue(int coins) =>
            _balancePlayerData.text = $"АСТРОБАКСОВ: {coins}";
        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}