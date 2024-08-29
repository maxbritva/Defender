using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class BossLevelStartFX : MonoBehaviour
    {
        [SerializeField] private GameObject _portalPrefab;
        private BackgroundColorsChanger _backgroundColorsChanger;
        private void Start() => _portalPrefab.SetActive(false);

        public void StartBossLevelFX(Transform targetTransform)
        {
            _backgroundColorsChanger.BossStateChangeColor(true);
            ActivatePortal(targetTransform);
            ActivateBossAnimation(targetTransform);
        }

        public void EndBossLevelFX() => _backgroundColorsChanger.BossStateChangeColor(false);

        private void ActivatePortal(Transform targetTransform) 
        {
            _portalPrefab.SetActive(true);
            _portalPrefab.gameObject.transform.position = targetTransform.position;
           // _portalPrefab.gameObject.transform.Rotate(new Vector3(90, 180, 0));
        }
        private void ActivateBossAnimation(Transform targetTransform) => targetTransform.DOScale(1f, 2.5f).SetEase(Ease.InOutSine);

        [Inject]
        private void Construct(BackgroundColorsChanger backgroundColorsChanger) => _backgroundColorsChanger = backgroundColorsChanger;
    }
}