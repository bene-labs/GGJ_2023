using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the gameplay of exactly one player
/// </summary>
public class PlayerObjectSpawner : MonoBehaviour
{
	[SerializeField]
	private RemovableRoot[] availableRoots;
	[SerializeField]
	private GroundSegment[] groundSegments;
	[SerializeField]
	private Viewport viewport;
	[SerializeField]
	private float rootSpacing = 0.25f;

	private List<GroundSegment> generatedGrounds = new();
	private List<RemovableRoot> generatedRoots = new();

	private float coveredToRight = 0;
	private float coveredToLeft = 0;

	private float nextRootStart = 0;

	public RemovableRoot CurrentRoot => this.generatedRoots.Count > 0 ? this.generatedRoots[0] : null;

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
		middleGround.transform.position = new Vector2(0, this.viewport.WorldSpaceDimensions.center.y - middleGround.dimensions.center.y);
		this.generatedGrounds.Add(middleGround);
		this.coveredToRight += middleGround.dimensions.width / 2;
		this.coveredToLeft -= middleGround.dimensions.width / 2;
		this.FillGroundToRight();
		this.FillGroundToLeft();

		var firstRoot = Object.Instantiate(this.availableRoots.GetRandom());
		firstRoot.transform.position = new Vector2(0, this.viewport.WorldSpaceDimensions.center.y);
		this.generatedRoots.Add(firstRoot);
		this.nextRootStart = firstRoot.Dimensions.width / 2 + this.rootSpacing;
		this.GenerateRoots();
	}

	private void FillGroundToRight()
	{
		while (this.coveredToRight < this.viewport.WorldSpaceDimensions.xMax)
		{
			var ground = Object.Instantiate(this.groundSegments.GetRandom());
			this.generatedGrounds.Add(ground);
			ground.transform.position = new Vector2(this.coveredToRight + ground.dimensions.width / 2, this.viewport.WorldSpaceDimensions.center.y - ground.dimensions.center.y);
			this.coveredToRight += ground.dimensions.width;
		}
	}
	private void FillGroundToLeft()
	{
		while (this.coveredToLeft > this.viewport.WorldSpaceDimensions.xMin)
		{
			var ground = Object.Instantiate(this.groundSegments.GetRandom());
			this.generatedGrounds.Add(ground);
			ground.transform.position = new Vector2(this.coveredToLeft - ground.dimensions.width / 2, this.viewport.WorldSpaceDimensions.center.y - ground.dimensions.center.y);
			this.coveredToLeft -= ground.dimensions.width;
		}
	}
	private void GenerateRoots()
	{
		while (this.nextRootStart < this.viewport.WorldSpaceDimensions.xMax)
		{
			var root = Object.Instantiate(this.availableRoots.GetRandom());
			root.transform.position = new Vector2(this.nextRootStart + root.Dimensions.width / 2, this.viewport.WorldSpaceDimensions.center.y);
			this.generatedRoots.Add(root);
			this.nextRootStart += this.rootSpacing + root.Dimensions.width;
		}
	}

	private void HandleViewportChange(Rect newRect)
	{
		Debug.LogFormat("new viewport: {0}", newRect);
	}

	// Update is called once per frame
	void Update()
	{
		// These methods only instantiate when necessary
		this.FillGroundToLeft();
		this.FillGroundToRight();
		this.GenerateRoots();
	}
}
