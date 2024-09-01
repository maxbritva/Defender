using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Health;
using Game.Weapons.Bonus;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class ShakeCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;
        private CinemachineBasicMultiChannelPerlin _channelPerlin;
        private CancellationTokenSource _cancellationToken;
        private readonly float _shakeIntensity = 4f;
        private PlayerHealth _playerHealth;
        private Bomb _bomb;

        private void OnEnable()
        {
            _channelPerlin = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            _cancellationToken = new CancellationTokenSource();
            _playerHealth.OnPlayerHit += CameraShake;
            _bomb.OnBombActivated += CameraShake;
        }

        private void OnDisable()
        {
            _playerHealth.OnPlayerHit -= CameraShake;
            _bomb.OnBombActivated -= CameraShake;
        }

        private async void CameraShake() => await Shake();

        private async UniTask Shake()
        {
            SetAmplitude(_shakeIntensity);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cancellationToken.IsCancellationRequested);
            SetAmplitude(0f);
            _cancellationToken.Cancel();
        }

        private void SetAmplitude(float value) => _channelPerlin.AmplitudeGain = value;
        [Inject] private void Construct(PlayerHealth playerHealth, Bomb bomb)
        {
            _playerHealth = playerHealth;
            _bomb = bomb;
        }
    }
}