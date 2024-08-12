using System.Collections;
using Player;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class ShakeCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;
        private CinemachineBasicMultiChannelPerlin _channelPerlin;
        private readonly float _shakeIntensity = 4f;
        private Coroutine _shakeCoroutine;
        private PlayerHealth _playerHealth;
        
        private void OnEnable()
        {
            _channelPerlin = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            _playerHealth.OnPlayerHit += CameraShake;
        }

        private void OnDisable()
        {
            _playerHealth.OnPlayerHit -= CameraShake;
            if(_shakeCoroutine != null)
                StopCoroutine(_shakeCoroutine);
        }

        public void CameraShake() => _shakeCoroutine = StartCoroutine(Shaking());

        private IEnumerator Shaking() {
           SetAmplitude(_shakeIntensity);
           yield return new WaitForSeconds(0.3f);
           SetAmplitude(0f);
        }

        private void SetAmplitude(float value) => _channelPerlin.AmplitudeGain = value;
        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}