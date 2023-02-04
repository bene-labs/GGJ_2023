using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the gameplay of exactly one player
/// </summary>
public class GameplayManager : MonoBehaviour
{
	[SerializeField]
	private RemovableRoot[] availableRoots;
	[SerializeField]
	private Viewport viewport;

	// TODO(rw): keep track of the score of one player

	void Start()
	{
		Debug.Assert(this.viewport != null, "GameplayManager: no viewport assigned!");
		this.InitializeFirstRoots();
	}

	private void InitializeFirstRoots()
	{
		// TODO(rw): initialize first few roots
	}

	// Update is called once per frame
	void Update()
	{

	}
}
