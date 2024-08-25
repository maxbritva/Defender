using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenu.Shop
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsFromWallet;

        private Wallet _wallet;

        private void OnEnable()
        {
            _wallet.Changed += UpdateValue;
            UpdateValue(_wallet.PlayerData.Balance);
        }
        private void OnDisable() => _wallet.Changed -= UpdateValue;

        private void UpdateValue(int coins) =>
            _coinsFromWallet.text = $"АСТРОБАКСОВ: {coins}";
        [Inject] private void Construct(Wallet wallet) => _wallet = wallet;
    }
}