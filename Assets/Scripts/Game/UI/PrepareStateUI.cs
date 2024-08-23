using Game.FX;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class PrepareStateUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private GameObject _prepareUIPanel;
        [SerializeField] private GameObject _GameUIPanel;
        [SerializeField] private GameObject _DesktopTips;
        [SerializeField] private GameObject _mobileTips;
        private PlayerData _playerData;
        private GameStartAnimation _gameStartAnimation;
        
        private void OnEnable()
        {
            _prepareUIPanel.SetActive(true);
            _startGameButton.onClick.AddListener(StartGameClick);
            if (_playerData.ShowTips)
            {
                if( SystemInfo.deviceType == DeviceType.Handheld)
                    _mobileTips.gameObject.SetActive(true);
                else
                    _DesktopTips.gameObject.SetActive(true);
            }
        }

        private void OnDisable() => _startGameButton.onClick.RemoveListener(StartGameClick);

        private void StartGameClick()
        {
            _prepareUIPanel.SetActive(false);
            _GameUIPanel.SetActive(true);
            _gameStartAnimation.StartAnimation();
        }

        [Inject] private void Construct(PlayerData playerData, GameStartAnimation animation)
        {
            _playerData = playerData;
            _gameStartAnimation = animation;
        }
    }
}