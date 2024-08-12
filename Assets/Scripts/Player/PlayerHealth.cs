using System;
using Game.Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public Action OnPlayerHit;
        public Action OnPlayerHeal;
        [SerializeField] private float _health = 100;

   

        public void TakeDamage(int value)
        {
            _health -= value;
            Debug.Log(_health);
        }
    }
}