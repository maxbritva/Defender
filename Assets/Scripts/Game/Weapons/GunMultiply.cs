using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

namespace Game.Weapons
{
    public class GunMultiply : GunBase, IWeapon
    {
	    [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
		[SerializeField] private List<GameObject> _muzzles = new List<GameObject>();
		private List<ParticleSystem> _muzzlesParticles = new List<ParticleSystem>();
		
		public void Shot(ObjectPool.Pool targetPool)
		{
			for (int i = 0; i < _shootPoints.Count; i++) {
				GameObject bulletFromPool = GetBulletFromPool(targetPool);
				bulletFromPool.transform.SetParent(transform);
				bulletFromPool.transform.position = _shootPoints[i].position;
				bulletFromPool.transform.rotation = _shootPoints[i].rotation;
			}
		}
		
    }
    }
