using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
	[SerializeField]
	private Viewport viewport;
	[SerializeField]
	private PlayerObjectSpawner spawner;

	void Start()
	{
		Debug.Assert(this.viewport != null, "PlayerGameplay: no viewport assigned!", this);
		Debug.Assert(this.spawner != null, "PlayerGameplay: no PlayerObjectSpawner assigned!", this);
	}

	void Update()
	{
		if (Input.GetButtonDown(InputActions.Neutral.ToInputName()))
		{
			if (this.spawner.CurrentRoot != null)
			{
				this.spawner.DestroyCurrentRoot();
			}
		}
		if (Input.GetButtonDown(InputActions.Down.ToInputName()))
		{
			Debug.Log("X");
		}
		if (Input.GetButtonDown(InputActions.Up.ToInputName()))
		{
			Debug.Log("Y");
		}
		if (Input.GetButtonDown(InputActions.Decline.ToInputName()))
		{
			Debug.Log("B");
		}
	}
}
