using System.Collections.Generic;
using UnityEngine;

namespace Player.Platform
{
    public class PlatformVisualChanger: MonoBehaviour
    {
        [SerializeField] private List<MeshFilter> _filters;
        [SerializeField] private List<Mesh> _meshes;
        
        public void ChooseLevel(int level)
        {
            for (int i = 0; i < _filters.Count; i++) 
                _filters[i].mesh = _meshes[level-1];
        }
    }
}