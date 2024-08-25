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
        [SerializeField] private GameObject _shop;
        [SerializeField] private TMP_Text _topScoreText;
        private ISceneLoadMediator _sceneLoader;
        private PlayerData _playerData;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameClick);
            _showShopButton.onClick.AddListener(ShowShopClick);
           UpdateTopScoreText();
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
            _showShopButton.onClick.RemoveListener(ShowShopClick);
        }
        
        private void StartGameClick() => _sceneLoader.StartGame();
        private void ShowShopClick() => _shop.gameObject.SetActive(true);

        private void UpdateTopScoreText() => _topScoreText.text = $"ТОП ОЧКОВ: { _playerData.TopScore}";


        [Inject] private void Construct(ISceneLoadMediator loadMediator, PlayerData playerData)
        {
            _sceneLoader = loadMediator;
            _playerData = playerData;
        }
    }
}