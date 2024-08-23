using System.Collections;
using DG.Tweening;
using Game.GameCore.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.FX
{
    public class GameStartAnimation : MonoBehaviour
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite[] _allSprites;
        [SerializeField] private Vector3 _scaleTo;
        private float _duration = 1f;
        private WaitForSeconds _interval;
        private GameManager _gameManager;

        private void Start() => _interval = new WaitForSeconds(_duration);

        public void StartAnimation() {
            _targetImage.gameObject.SetActive(true);
            StartCoroutine(PrepareForBattle());
            ApplyAnimation();
        }

        private IEnumerator PrepareForBattle() {
            for (int i = 0; i < 4; i++) {
                _targetImage.sprite = _allSprites[i];
                ApplyAnimation();
                yield return _interval;
            }
            _gameManager.OnGameStarted?.Invoke();
            _targetImage.gameObject.SetActive(false);
        }

        private void ApplyAnimation() => _targetImage.transform.DOScale(_scaleTo, _duration).SetLoops(-1, LoopType.Restart);

        [Inject]
        private void Construct(GameManager gameManager) => _gameManager = gameManager;
    }
}