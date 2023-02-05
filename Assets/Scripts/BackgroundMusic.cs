using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{

	private static BackgroundMusic instance;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		instance = null;
	}

	[SerializeField]
	private AudioClip[] clips;
	[SerializeField]
	private float minDelay = 0.5f;
	[SerializeField]
	private float maxDelay = 2;

	private AudioSource audioSouce;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			this.audioSouce = this.GetComponent<AudioSource>();
		}
		else
		{
			Object.Destroy(this.gameObject);
		}
	}

	void Update()
	{
		if (!this.audioSouce.isPlaying)
		{
			var delay = Random.Range(minDelay, maxDelay);
			this.audioSouce.clip = this.clips.GetRandom();
			this.audioSouce.PlayDelayed(delay);
		}
	}
}
