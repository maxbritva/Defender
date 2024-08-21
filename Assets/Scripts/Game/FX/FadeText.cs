using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;


namespace Game.FX
{
    public class FadeText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _timeToFade;
        private bool _isFullAlpha = true;
        private Coroutine _fullAlpha;
        private Coroutine _zeroAlpha;
        private void OnEnable()
        {
            _text.DOFade(0, _timeToFade).Play().SetLoops(-1);
        }

       
    }
}