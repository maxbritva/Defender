using Audio;
using Player;
using Save;
using Zenject;

namespace MainMenu.Settings
{
    public class SettingsMediator
    {
        private PlayerData _playerData;
        private DataProvider _dataProvider;
        private AudioManager _audioManager;
        
        public void ChangeSoundEnableValue()
        {
            _playerData.SetSound(!_playerData.EnabledSound);
            _audioManager.SetSoundVolume();
            _dataProvider.Save();
        }

        public void ChangeShowTipsValue()
        {
            _playerData.SetShowTips(!_playerData.ShowTips);
            _dataProvider.Save();
        }

        public void ResetData()
        {
            _playerData.ResetData();
            _dataProvider.Save();
        }

        [Inject] private void Construct(PlayerData playerData, DataProvider dataProvider, AudioManager audioManager)
        {
            _audioManager = audioManager;
            _playerData = playerData;
            _dataProvider = dataProvider;
        }
    }
}