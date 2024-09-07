using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.Menu
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _showShopButton;
        [SerializeField] private Button _showSettingsButton;
        [Header("panels")]
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private GameObject _settingsPanel;
        
        private MainMenuMediator _mainMenuMediator;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameClick);
            _showShopButton.onClick.AddListener(ShowShopClick);
            _showSettingsButton.onClick.AddListener(ShowSettingsClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
            _showShopButton.onClick.RemoveListener(ShowShopClick);
            _showSettingsButton.onClick.RemoveListener(ShowSettingsClick);
        }

        private void StartGameClick() => _mainMenuMediator.StartGame();

        private void ShowShopClick() => _shopPanel.gameObject.SetActive(true);

        private void ShowSettingsClick() => _settingsPanel.SetActive(true);

        [Inject] private void Construct(MainMenuMediator mainMenuMediator) => _mainMenuMediator = mainMenuMediator;
    }
}