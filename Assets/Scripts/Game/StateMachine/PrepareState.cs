using System;
using Player;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace Game.StateMachine
{
    public class PrepareState : MonoBehaviour
    {
        private PlayerData _playerData;
        private PlatformInitialize _platformInitialize;

        private void Awake()
        {
            _platformInitialize.Initialize(_playerData.PlatformGunLevel);
        }

      

        [Inject] private void Construct(PlayerData data, PlatformInitialize platform)
        {
            _playerData = data;
            _platformInitialize = platform;
        }
    }
}