using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.Shop
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _textPrice;
        [SerializeField] private TMP_Text _textLevel;
        [SerializeField] private string _tag;
        private Shop _shop;
        public string Tag => _tag;
        
        private void OnEnable() => _buyButton.onClick.AddListener(BuyButtonClick);

        private void OnDisable() => _buyButton.onClick.RemoveListener(BuyButtonClick);

        private void BuyButtonClick() => _shop.BuyUpgrade(_tag);

        public void UpdatePrice(int value) => _textPrice.text = $"COST: {value}";
        public void UpdateLevel(int value) => _textLevel.text = value.ToString();

        public void IsLockCheck(bool value) => _buyButton.interactable = value;

        [Inject] private void Construct(Shop shop) => _shop = shop;
    }

}