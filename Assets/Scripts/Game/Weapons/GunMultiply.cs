using System.Collections.Generic;
using Game.Interfaces;
using Game.ObjectPool;
using UnityEngine;

namespace Game.Weapons
{
    public class GunMultiply : MonoBehaviour, IWeapon
    {
	    [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
	    
	    public void Shot(Pool projectilePool)
		{
			for (int i = 0; i < _shootPoints.Count; i++) {
				GameObject bulletFromPool = projectilePool.GetFromPool();
					bulletFromPool.transform.SetParent(transform);
				bulletFromPool.transform.position = _shootPoints[i].position;
				bulletFromPool.transform.rotation = _shootPoints[i].rotation;
			}
		}
    }
    }
