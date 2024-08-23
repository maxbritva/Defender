using System.IO;
using Newtonsoft.Json;
using Player;
using UnityEngine;
using Zenject;

namespace Save
{
    public class DataProvider : IDataProvider
    {
        private const string FileName = "PlayerSaveData";
        private const string SaveFileExtension = ".json";
        
        private string SavePath => Application.persistentDataPath;
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

        private PlayerData _playerData;
        
        public DataProvider(PlayerData playerData) => _playerData = playerData;

        public void Save()
        {
         File.WriteAllText(FullPath, JsonConvert.SerializeObject(_playerData, Formatting.Indented, new JsonSerializerSettings
         {
             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
         }));
        }

        public bool TryLoad()
        {
            if (IsDataAlreadyExist() == false)
                return false;
            _playerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));
            return true;
        }

        private bool IsDataAlreadyExist() => File.Exists(FullPath);

        // [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}