using System;
using Player;
using Zenject;

namespace MainMenu.Shop
{
    public class Wallet
    {
        public event Action<int> Changed;
        private PlayerData _playerData;

        public PlayerData PlayerData => _playerData;

        public void Add(int coins)
        {
            _playerData.AddBalance(coins);
            Changed?.Invoke(_playerData.Balance);
        }

        public bool IsEnough(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins)); 
            return  _playerData.Balance >= coins;
        }

        public void Spend(int coins)
        {
            _playerData.SpendBalance(coins);
            Changed?.Invoke(_playerData.Balance);
        }

        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}