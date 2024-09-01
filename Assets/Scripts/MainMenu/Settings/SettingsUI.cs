using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _showTipsButton;
        [SerializeField] private Button _EnableSoundButton;
        [SerializeField] private Button _resetButton;
        [Header("texts")]
        [SerializeField] private TMP_Text _tipsText;
        [SerializeField] private TMP_Text _soundText;

        private SettingsMediator _settingsMediator;
        private PlayerData _playerData;

        private void Start()
        {
            SetTipsText(_playerData.ShowTips);
            SetSoundText(_playerData.EnabledSound);
        }

        private void OnEnable()
        {
            _showTipsButton.onClick.AddListener(ShowTipsButtonClick);
            _EnableSoundButton.onClick.AddListener(EnableSoundButtonClick);
            _resetButton.onClick.AddListener(ResetDataButtonClick);
            
        }

        private void OnDisable()
        {
            _showTipsButton.onClick.RemoveListener(ShowTipsButtonClick);
            _EnableSoundButton.onClick.RemoveListener(EnableSoundButtonClick);
            _resetButton.onClick.RemoveListener(ResetDataButtonClick);
        }

        private void ShowTipsButtonClick()
        {
            _settingsMediator.ChangeShowTipsValue();
            SetTipsText(_playerData.ShowTips);
        }

        private void EnableSoundButtonClick()
        {
            _settingsMediator.ChangeSoundEnableValue();
            SetSoundText(_playerData.EnabledSound);
        }

        private void ResetDataButtonClick() => _settingsMediator.ResetData();

        private void SetTipsText(bool enable) => _tipsText.text = 
            enable ? "Disable tips" : "Enable tips";
        
        private void SetSoundText(bool enable) => _soundText.text = 
            enable ? "Turn off sounds" : "Turn on sounds";

        [Inject]
        private void Construct(SettingsMediator settingsMediator, PlayerData playerData)
        {
            _settingsMediator = settingsMediator;
            _playerData = playerData;
        }
    }
}