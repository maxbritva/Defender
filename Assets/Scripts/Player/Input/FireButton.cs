using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.Input
{
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isFire;

        public void OnPointerDown(PointerEventData eventData) => _isFire = true;

        public void OnPointerUp(PointerEventData eventData) => _isFire = false;

        public bool isFireButtonOn() => _isFire;
    }
}