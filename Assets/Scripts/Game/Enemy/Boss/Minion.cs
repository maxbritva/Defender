using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Boss
{
    public class Minion: MonoBehaviour, IPause
    {
        private PauseHandler _pauseHandler;
        private Coroutine _minionCoroutine;
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
            if(_minionCoroutine != null)
                StopCoroutine(Attack());
        }
        private void ActivateMinion() {
            gameObject.transform.position = Random.insideUnitCircle.normalized * 24.0f;
            transform.LookAt(Vector3.zero, Vector3.forward * -1);
            gameObject.SetActive(true);
            _minionCoroutine =  StartCoroutine(Attack());
        }
        private IEnumerator Attack() 
        {
            while (true) {
                if(_isPaused == false)
                    transform.position = Vector3.MoveTowards(transform.position,Vector3.zero, Speed * Time.deltaTime);
                yield return null;
            }
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        [Inject]
        private void Construct(PauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }
    }
}