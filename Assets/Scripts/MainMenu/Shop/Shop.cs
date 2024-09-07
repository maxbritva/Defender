using System;
using Player;
using Save;
using UnityEngine;
using Zenject;

namespace MainMenu.Shop
{
    public class Shop : MonoBehaviour
    {
        public event Action OnBuyUpgrade;
        
        private PlayerData _playerData;
        private IDataProvider _dataProvider;
        private UpgradesHandler _upgradesHandler;
        
        public void BuyUpgrade(string tag)
        {
            if (_playerData.IsEnough(_upgradesHandler.Upgrades[(tag, _playerData.GetUpgradeLevel(tag))].Cost))
            {
                _playerData.SpendBalance(_upgradesHandler.Upgrades[(tag, _playerData.GetUpgradeLevel(tag))].Cost);
                _playerData.SetUpgradeLevel(tag, _upgradesHandler.Upgrades[(tag, _playerData.GetUpgradeLevel(tag))].Level + 1);
                SuccessBuy(); 
            }
        }

        private void SuccessBuy()
        {
            OnBuyUpgrade?.Invoke();
            _dataProvider.Save();
        }

        [Inject] private void Construct(PlayerData playerData, 
            IDataProvider dataProvider, UpgradesHandler upgradesHandler)
        {
            _playerData = playerData;
            _dataProvider = dataProvider;
            _upgradesHandler = upgradesHandler;
        }
    }
}