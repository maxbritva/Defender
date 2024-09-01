using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class CloseButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panelToClose;

        private void OnEnable() => _closeButton.onClick.AddListener(ClosePanel);

        private void OnDisable() => _closeButton.onClick.RemoveListener(ClosePanel);

        private void ClosePanel() => _panelToClose.gameObject.SetActive(false);

    }
}