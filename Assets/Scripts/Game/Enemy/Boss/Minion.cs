using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Boss
{
    public class Minion: MonoBehaviour, IPause
    {
        private BossSpawner _bossSpawner;
        private PauseHandler _pauseHandler;
        private Coroutine _minionCoroutine;
        private CancellationTokenSource _cts;
        private const float Speed = 4f;
        private bool _isPaused;
        
        private void OnEnable() 
        {
            _pauseHandler.Add(this);
            ActivateMinion();
        }
        private void OnDisable() 
        {
            _pauseHandler.Remove(this);
            _bossSpawner.RemoveMinionFromList(gameObject);
            _cts.Cancel();
        }
        private async void ActivateMinion() 
        {
            gameObject.transform.position = Random.insideUnitCircle.normalized * 24.0f;
            transform.LookAt(Vector3.zero, Vector3.forward * -1);
            gameObject.SetActive(true);
            _cts = new CancellationTokenSource();
            await Attack().SuppressCancellationThrow();
        }
        
        private async UniTask Attack()
        {
            
            while (gameObject.activeInHierarchy)
            {
                if (_isPaused == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position,Vector3.zero, Speed * Time.deltaTime);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        [Inject]
        private void Construct(PauseHandler pauseHandler, BossSpawner bossSpawner)
        {
            _bossSpawner = bossSpawner;
            _pauseHandler = pauseHandler;
        }
    }
}