using Player;
using UnityEngine;
using Zenject;

namespace Game.StateMachine
{
    public class PrepareState : MonoBehaviour
    {
        [SerializeField] private GameObject _platformShoot;
        private PlayerData _playerData;
        

        [Inject] private void Construct(PlayerData data)
        {
            _playerData = data;
          
        }
    }
}