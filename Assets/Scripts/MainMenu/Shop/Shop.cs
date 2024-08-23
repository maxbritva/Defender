using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;

namespace MainMenu.Shop
{
    public class Shop : MonoBehaviour
    {
        private PlayerData _playerData;
        private Wallet _wallet;

        private void Start() => CurrentUpgradesListSetup();

        private void CurrentUpgradesListSetup()
        {
        
        }

    

        [Inject] private void Construct(PlayerData playerData, Wallet wallet)
        {
            _playerData = playerData;
        }
        
       
    }
}