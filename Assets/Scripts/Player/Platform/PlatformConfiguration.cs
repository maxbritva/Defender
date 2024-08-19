using System.Collections.Generic;
using UnityEngine;

namespace Player.Platform
{
    public class PlatformConfiguration : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabPlatforms = new List<GameObject>();
        [SerializeField] private Transform _placerPlatform;

        public List<GameObject> PrefabPlatforms => _prefabPlatforms;
        public Transform PlacerPlatform => _placerPlatform;
    }
}