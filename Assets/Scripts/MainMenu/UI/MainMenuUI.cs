using System;
using MainMenu.Shop;
using Player;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _showShopButton;
        [SerializeField] private Button _showSettingsButton;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private TMP_Text _topScoreText;
        private ISceneLoadMediator _sceneLoader;
        private PlayerData _playerData;
        private BalanceView _balanceView;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameClick);
            _showShopButton.onClick.AddListener(ShowShopClick);
            _showSettingsButton.onClick.AddListener(ShowSettingsClick);
        }

        private void Start()
        {
            UpdateTopScoreText();
            _balanceView.UpdateValue(_playerData.Balance);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
            _showShopButton.onClick.RemoveListener(ShowShopClick);
            _showSettingsButton.onClick.RemoveListener(ShowSettingsClick);
        }
        
        private void StartGameClick() => _sceneLoader.StartGame();
        private void ShowShopClick() => _shopPanel.gameObject.SetActive(true);

        private void ShowSettingsClick() => _settingsPanel.SetActive(true);

        private void UpdateTopScoreText() => _topScoreText.text = $"ТОП ОЧКОВ: { _playerData.TopScore}";


        [Inject] private void Construct(BalanceView balanceView, ISceneLoadMediator loadMediator, PlayerData playerData)
        {
            _sceneLoader = loadMediator;
            _playerData = playerData;
            _balanceView = balanceView;
        }
    }
}