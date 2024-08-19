using Game.Enemy;
using Game.GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Game.GameCore
{
    public class GameBootStrap: MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        private PauseHandler _pauseHandler;

        private void Initialize()
        {
            _pauseHandler.SetPause(false); 
        }

        [Inject]  private void Constuct(PauseHandler pauseHandler) => 
            _pauseHandler = pauseHandler;
    }
}