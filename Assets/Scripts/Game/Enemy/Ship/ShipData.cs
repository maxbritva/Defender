using System;

namespace Game.Enemy.Ship
{
    public class ShipData
    {
        private float _speed;

        public float Speed
        {
            get => _speed;
            set
            {
                if(value <0)
                    throw  new ArgumentOutOfRangeException(nameof(value));
                _speed = value;
            }
        }
    }
}