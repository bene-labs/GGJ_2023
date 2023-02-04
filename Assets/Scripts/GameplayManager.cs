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
	private GroundSegment[] groundSegments;
	[SerializeField]
	private Viewport viewport;

	private List<GroundSegment> generatedGrounds = new List<GroundSegment>();

	private float coveredToRight = 0;
	private float coveredToLeft = 0;

	// TODO(rw): keep track of the score of one player

	void Start()
	{
		Debug.Assert(this.viewport != null, "GameplayManager: no viewport assigned!");
		Debug.Assert(this.groundSegments != null && this.groundSegments.Length > 0, "GameplayManager: no viewport assigned!");
		this.viewport.ViewportChanged += this.HandleViewportChange;
		this.InitializeFirstRoots();
	}

	private void InitializeFirstRoots()
	{
		var test = this.viewport.WorldSpaceDimensions;
		Debug.LogFormat("viewport: {0}", test);
		var middleGround = Object.Instantiate(this.groundSegments.GetRandom());
		middleGround.transform.position = new Vector2(0, -middleGround.dimensions.center.y);
		this.coveredToRight += middleGround.dimensions.width / 2;
		this.coveredToLeft -= middleGround.dimensions.width / 2;
		this.FillToRight();
		this.FillToLeft();
	}

	private void FillToRight()
	{
		while (this.coveredToRight < this.viewport.WorldSpaceDimensions.xMax)
		{
			var ground = Object.Instantiate(this.groundSegments.GetRandom());
			ground.transform.position = new Vector2(this.coveredToRight + ground.dimensions.width / 2, -ground.dimensions.center.y);
			this.coveredToRight += ground.dimensions.width;
		}
	}
	private void FillToLeft()
	{
		while (this.coveredToLeft > this.viewport.WorldSpaceDimensions.xMin)
		{
			var ground = Object.Instantiate(this.groundSegments.GetRandom());
			ground.transform.position = new Vector2(this.coveredToLeft - ground.dimensions.width / 2, -ground.dimensions.center.y);
			this.coveredToLeft -= ground.dimensions.width;
		}
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
