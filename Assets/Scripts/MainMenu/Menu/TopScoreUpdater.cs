using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenu.UI
{
    public class TopScoreUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _topScoreText;
        
        private PlayerData _playerData;

        private void Start() => UpdateTopScoreText();

        private void UpdateTopScoreText() => _topScoreText.text = $"TOP SCORE: { _playerData.TopScore}";
        
        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}