using System;
using System.Collections;
using UnityEngine;

namespace Game.Weapons.Bonus
{
    public class Bomb : MonoBehaviour
    {
        public event Action OnBombActivated;
        private Coroutine _bombCoroutine;
        private WaitForSeconds _tick = new WaitForSeconds(1f);

        private void OnEnable() => StartCoroutine(BombLifeTime());

        private IEnumerator BombLifeTime()
        {
            OnBombActivated?.Invoke();
            yield return _tick;
           gameObject.SetActive(false);
        }
    }
}