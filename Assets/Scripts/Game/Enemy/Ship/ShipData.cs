using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy.Ship
{
    public class ShipData
    {
        public List<Vector3> WayPoints = new List<Vector3>();
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