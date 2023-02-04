using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyParticles : MonoBehaviour
{
	private new ParticleSystem particleSystem;
	[SerializeField]
	private GameObject target;
	private GameObject effectiveTarget => this.target != null ? this.target : this.gameObject;

	void Awake()
	{
		this.particleSystem = this.GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if (this.particleSystem.isStopped)
		{
			Object.Destroy(this.effectiveTarget);
		}
	}
}
