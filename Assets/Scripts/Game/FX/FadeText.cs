using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.FX
{
    public class FadeText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _timeToFade;
       
        private void OnEnable() => _text.DOFade(0, _timeToFade).Play().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}