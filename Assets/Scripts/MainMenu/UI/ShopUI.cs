using System.Collections.Generic;
using MainMenu.Shop;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private TMP_Text _platformGunLevelText;
        [SerializeField] private TMP_Text _livesCountLevelText;
        [SerializeField] private TMP_Text _shieldTimerLevelText;
        [SerializeField] private TMP_Text _shootRateLevelText;
        [SerializeField] private TMP_Text _damageLevelText;
        [SerializeField] private TMP_Text _critLevelText;
        private Wallet _wallet;
        private PlayerData _playerData;
        private UpgradesHandler _upgradesHandler;
        private Shop.Shop _shop;
     
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseShopClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseShopClick);
        }

        private void CloseShopClick() => _shopPanel.gameObject.SetActive(false);
        


        [Inject] private void Construct(Wallet wallet, UpgradesHandler upgradesHandler)
        {
            _wallet = wallet;

            _upgradesHandler = upgradesHandler;
        }
    }
}