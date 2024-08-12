using UnityEngine;
using UnityEngine.UI;

namespace Game.FX
{
    public class BackgroundScroll : MonoBehaviour
    {
        private RawImage _rawImage;
        [SerializeField]private float _scrollSpeed = 0.01f;
        [SerializeField]private float _xDirection = 1f;
        [SerializeField]private float _yDirection = 1f;

        private void Awake() => _rawImage = GetComponent<RawImage>();

        private void Update()
        {
            _rawImage.uvRect = new Rect(_rawImage.uvRect.position + 
                                        new Vector2(_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) 
                                        * Time.deltaTime, _rawImage.uvRect.size);
        }
    }
}