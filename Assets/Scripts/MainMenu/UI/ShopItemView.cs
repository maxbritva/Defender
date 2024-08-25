using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textPrice;
        [SerializeField] private TMP_Text _textLevel;

        public void UpdatePrice(int value) => _textPrice.text = $"ЦЕНА: {value}";
        public void UpdateLevel(int value) => _textLevel.text = value.ToString();

        public void IsLockCheck(bool value) => _button.interactable = value;
    }

}