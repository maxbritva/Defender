using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _shopPanel;

        private void OnEnable() => _closeButton.onClick.AddListener(CloseShopClick);

        private void OnDisable() => _closeButton.onClick.RemoveListener(CloseShopClick);

        private void CloseShopClick() => _shopPanel.gameObject.SetActive(false);
        
     
    }
}