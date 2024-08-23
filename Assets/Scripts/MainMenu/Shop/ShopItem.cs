using UnityEngine;

namespace MainMenu.Shop
{
    [CreateAssetMenu(fileName = "ItemShop", menuName = "ScriptableObjects")]
    public class ShopItem : ScriptableObject
    {
        [SerializeField, Range(0.1f,1000)] private float _value;
        [SerializeField, Range(0,10000)] private int _cost;
        [SerializeField, Range(1,5)] private int _level;

        public float Value => _value;
        public int Cost => _cost;
        public int Level => _level;
    }
}