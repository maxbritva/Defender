using Player;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private AudioMixer _audioMixer;
        private PlayerData _playerData;

        private void Start()
        {
            _audioSource.clip = _menuMusic;
            SetSoundVolume();
        }

        public void SetSoundVolume()
        {
            if (_playerData.EnabledSound)
            {
                _audioMixer.SetFloat("Volume", -6f);
                _audioSource.Play();
            }
            else
            {
                _audioMixer.SetFloat("Volume", -80f);
                _audioSource.Stop();
            }
        }

        public void PlayGameMusic() {
            _audioSource.Stop();
            _audioSource.clip = _gameMusic;
           SetSoundVolume();
        }
        public void PlayMenuMusic() {
            _audioSource.Stop();
            _audioSource.clip = _menuMusic;
            SetSoundVolume();
        }

        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}