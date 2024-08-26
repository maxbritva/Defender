using System;
using MainMenu.Settings;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.UI
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Button _showTipsButton;
        [SerializeField] private Button _EnableSoundButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _tipsText;
        [SerializeField] private TMP_Text _soundText;
        private SettingsHandler _settingsHandler;
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
            _settingsHandler.ChangeShowTipsValue();
            SetTipsText(_playerData.ShowTips);
        }

        private void EnableSoundButtonClick()
        {
            _settingsHandler.ChangeSoundEnableValue();
            SetSoundText(_playerData.EnabledSound);
        }

        private void ResetDataButtonClick() => _settingsHandler.ResetData();

        private void SetTipsText(bool enable) => _tipsText.text = 
            enable ? "Отключить подсказки" : "Включить подсказки";
        
        private void SetSoundText(bool enable) => _soundText.text = 
            enable ? "Отключить звуки" : "Включить звуки";

        [Inject]
        private void Construct(SettingsHandler settingsHandler, PlayerData playerData)
        {
            _settingsHandler = settingsHandler;
            _playerData = playerData;
        }
    }
}