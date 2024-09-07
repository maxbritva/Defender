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
                    _playerData.SetUpgradeLevel("Platform", loadedData.PlatformGunLevel);
                    _playerData.SetUpgradeLevel("Lives",loadedData.LivesCountLevel); 
                    _playerData.SetUpgradeLevel("Shield",loadedData.ShieldTimerLevel); 
                    _playerData.SetUpgradeLevel("ShootRate",loadedData.ShootRateLevel); 
                    _playerData.SetUpgradeLevel("Damage",loadedData.DamageLevel); 
                    _playerData.SetUpgradeLevel("Crit",loadedData.CritLevel);
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
            _playerData.SetUpgradeLevel("Platform", 1);
            _playerData.SetUpgradeLevel("Lives",1); 
            _playerData.SetUpgradeLevel("Shield",1); 
            _playerData.SetUpgradeLevel("ShootRate",1); 
            _playerData.SetUpgradeLevel("Damage",1); 
            _playerData.SetUpgradeLevel("Crit",1);
            _playerData.SetShowTips(true);
            _playerData.SetSound(true);
        }

        private bool IsDataAlreadyExist() => File.Exists(FullPath);

         [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
      
    }
}