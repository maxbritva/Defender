using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class CloseButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panelToClose;

        private void OnEnable() => _closeButton.onClick.AddListener(CloseShopClick);

        private void OnDisable() => _closeButton.onClick.RemoveListener(CloseShopClick);

        private void CloseShopClick() => _panelToClose.gameObject.SetActive(false);
        
     
    }
}