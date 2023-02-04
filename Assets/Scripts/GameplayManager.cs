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
		this.viewport.ViewportChanged += this.HandleViewportChange;
	}

	private void InitializeFirstRoots()
	{
		var test = this.viewport.WorldSpaceDimensions;
		Debug.LogFormat("viewport: {0}", test);
		// TODO(rw): initialize first few roots
	}

	private void HandleViewportChange(Rect newRect)
	{
		Debug.LogFormat("new viewport: {0}", newRect);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
