using UnityEngine;

namespace Game.FX
{
    public class PlatformFreezeFX : MonoBehaviour
    {
        [SerializeField] private GameObject _freezeGameObject;
        public void ShowFreezeFX() => _freezeGameObject.SetActive(true);
    }
}