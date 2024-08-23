using SceneLoader;
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
        private ISceneLoadMediator _sceneLoader;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameClick);
            _showShopButton.onClick.AddListener(ShowShopClick);
           
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
            _showShopButton.onClick.RemoveListener(ShowShopClick);
        }
        
        private void StartGameClick() => _sceneLoader.StartGame();
        private void ShowShopClick() => _shop.gameObject.SetActive(true);
        

        [Inject] private void Construct(ISceneLoadMediator loadMediator) => _sceneLoader = loadMediator;
    }
}