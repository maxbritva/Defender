using Player;
using Save;
using Zenject;

namespace MainMenu.Settings
{
    public class SettingsHandler
    {
        private PlayerData _playerData;
        private DataProvider _dataProvider;
        

        public void ChangeSoundEnableValue()
        {
            _playerData.SetSound(!_playerData.EnabledSound);
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
        


        [Inject] private void Construct(PlayerData playerData, DataProvider dataProvider)
        {
            _playerData = playerData;
            _dataProvider = dataProvider;
        }
    }
}