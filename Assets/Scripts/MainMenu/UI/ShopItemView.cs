using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class ShopItemView : MonoBehaviour
    {
        public event Action Click;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;
        private float _lockAnimationDuration = 0.4f;
        private float _lockAnimationStrength = 2f;

        private bool _isLock;

        // private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        //
        // private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void UpdatePrice(int value)
        {
            _text.text = $"ЦЕНА: {value}";
        }

        public void IsLockCheck(bool value)
        {
            if (value)
            {
                _isLock = true;
                _text.color = _lockColor;
                _button.GetComponent<Image>().color = _lockColor;
                _button.interactable = false;
            }
            else
            {
               _isLock = false;
                _text.color = _unlockColor;
                _button.GetComponent<Image>().color = _unlockColor;
                _button.interactable = true;
            }
        }

        // private void OnButtonClick()
        // {
        //     if (_isLock)
        //     {
        //         transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
        //         return;
        //     }
        //     Click?.Invoke();
        // }
    }

}