using System.Collections.Generic;
using Game.ObjectPool;
using UnityEngine;

namespace Game.Weapons
{
    public class GunMultiply : MonoBehaviour
    {
	    [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
	    [SerializeField] private GameObject _prefabProjectile;
	    
	    public void Shot(GameObjectPool projectilePool)
		{
			for (int i = 0; i < _shootPoints.Count; i++) 
			
			{
				GameObject bulletFromPool = projectilePool.GetFromPool(_prefabProjectile);
				bulletFromPool.transform.position = _shootPoints[i].position;
				bulletFromPool.transform.rotation = _shootPoints[i].rotation;
			}
		}
    }
    }
