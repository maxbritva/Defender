using System.IO;
using Newtonsoft.Json;
using Player;
using UnityEngine;
using Zenject;

namespace Save
{
    public class DataProvider : IDataProvider, IInitializable
    {
        private const string FileName = "PlayerSaveData";
        private const string SaveFileExtension = ".json";
        
        private string SavePath => Application.persistentDataPath;
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

        private PlayerData _playerData;

        public void Save() => File.WriteAllText(FullPath, JsonConvert.SerializeObject(_playerData, Formatting.Indented));

        public void Initialize() => TryLoad();
        public bool TryLoad()
        {
            if (IsDataAlreadyExist() == false)
            {
                SetDefaultPlayerData();
                return false;
            }
            else
            {
                var loadedData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));
                if (loadedData != null)
                {
                    _playerData.SetBalance(loadedData.Balance);
                    _playerData.SetTopScore(loadedData.TopScore);
                    _playerData.SetPlatformGunLevel(loadedData.PlatformGunLevel); 
                    _playerData.SetLivesCountLevel(loadedData.LivesCountLevel); 
                    _playerData.SetShieldTimerLevel(loadedData.ShieldTimerLevel); 
                    _playerData.SetShootRateLevel(loadedData.ShootRateLevel); 
                    _playerData.SetDamageLevelLevel(loadedData.DamageLevel); 
                    _playerData.SetCritLevel(loadedData.CritLevel);
                    _playerData.SetShowTips(loadedData.ShowTips);
                    _playerData.SetSound(loadedData.EnabledSound);
                }
                else
                    SetDefaultPlayerData();
                return true;
            }
        }

        private void SetDefaultPlayerData()
        {
            _playerData.SetBalance(1000);
            _playerData.SetTopScore(0);
            _playerData.SetPlatformGunLevel(1);
            _playerData.SetLivesCountLevel(1);
            _playerData.SetShieldTimerLevel(1);
            _playerData.SetShootRateLevel(1);
            _playerData.SetDamageLevelLevel(1);
            _playerData.SetCritLevel(1);
            _playerData.SetShowTips(true);
            _playerData.SetSound(true);
        }

        private bool IsDataAlreadyExist() => File.Exists(FullPath);

         [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
      
    }
}