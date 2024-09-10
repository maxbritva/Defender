using System.Collections.Generic;
using UnityEngine;

namespace Game.Bonus
{
    [CreateAssetMenu(fileName = "BonusesConfiguration", menuName = "ScriptableObjects/BonusesConfiguration")]
    public class BonusesConfiguration : ScriptableObject
    {
        [SerializeField] private List<BonusBase> _bonusPrefabs = new List<BonusBase>();
        
        [SerializeField] private List<Vector3> _spawnPoints = new List<Vector3>()
        {
            new Vector3(20,0,0),
            new Vector3(20,11,0),
            new Vector3(20,-11,0),
            new Vector3(-20,0,0),
            new Vector3(-20,11,0),
            new Vector3(-20,-11,0),
            new Vector3(-7,11,0),
            new Vector3(7,11,0),
            new Vector3(7,-11,0),
            new Vector3(-7,-11,0),
        };
        public List<BonusBase> BonusPrefabs => _bonusPrefabs;

        public List<Vector3> SpawnPoints => _spawnPoints;
    }
}