﻿using System;
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

        private void Start()
        {
            _upgradesHandler.LoadUpgrades();
            _upgradesHandler.UpdateCurrentUpgrades();
        }

        public void BuyPlatformUpgrade()
        {
            if (_playerData.IsEnough(_upgradesHandler.PlatformCurrentLevel.Cost))
            {
                _playerData.SpendBalance(_upgradesHandler.PlatformCurrentLevel.Cost);
                _playerData.SetPlatformGunLevel(_upgradesHandler.PlatformCurrentLevel.Level + 1);
                SuccessBuy(); 
            }
        }
        public void BuyLivesUpgrade()
        {
            if(_playerData.IsEnough(_upgradesHandler.LivesCurrentLevel.Cost))
                _playerData.SpendBalance(_upgradesHandler.LivesCurrentLevel.Cost);
            _playerData.SetLivesCountLevel(_upgradesHandler.LivesCurrentLevel.Level + 1);
            SuccessBuy();
        }
        
        public void BuyShieldUpgrade()
        {
            if(_playerData.IsEnough(_upgradesHandler.ShieldCurrentLevel.Cost))
                _playerData.SpendBalance(_upgradesHandler.ShieldCurrentLevel.Cost);
            _playerData.SetShieldTimerLevel(_upgradesHandler.ShieldCurrentLevel.Level + 1);
            SuccessBuy();
        }
        public void BuyShootRateUpgrade()
        {
            if(_playerData.IsEnough(_upgradesHandler.ShootRateCurrentLevel.Cost))
                _playerData.SpendBalance(_upgradesHandler.ShootRateCurrentLevel.Cost);
            _playerData.SetShootRateLevel(_upgradesHandler.ShootRateCurrentLevel.Level + 1);
            SuccessBuy();
        }
        public void BuyDamageUpgrade()
        {
            if(_playerData.IsEnough(_upgradesHandler.DamageCurrentLevel.Cost))
                _playerData.SpendBalance(_upgradesHandler.DamageCurrentLevel.Cost);
            _playerData.SetDamageLevelLevel(_upgradesHandler.DamageCurrentLevel.Level + 1);
            SuccessBuy();
        }
        public void BuyCritUpgrade()
        {
            if(_playerData.IsEnough(_upgradesHandler.CritCurrentLevel.Cost))
                _playerData.SpendBalance(_upgradesHandler.CritCurrentLevel.Cost);
            _playerData.SetCritLevel(_upgradesHandler.CritCurrentLevel.Level + 1);
            SuccessBuy();
        }
        
        private void SuccessBuy()
        {
            Debug.Log(_playerData.Balance);
            _upgradesHandler.UpdateCurrentUpgrades();
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