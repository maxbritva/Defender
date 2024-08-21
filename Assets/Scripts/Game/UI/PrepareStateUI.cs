using SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PrepareStateUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;

        private void OnEnable() => _startGameButton.onClick.AddListener(StartGameClick);

        private void OnDisable() => _startGameButton.onClick.RemoveListener(StartGameClick);

        private void StartGameClick()
        {
            
        }

    }
}