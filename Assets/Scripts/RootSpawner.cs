using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RootSpawner : IGameplaySection
{
	[SerializeField]
	private float spawnDelaySeconds = 0.05f;
	[SerializeField]
	private float despawnDelaySeconds = 0.05f;
	private RemovableRoot currentRoot;

	[Header("Growing")]
	[SerializeField]
	private AnimationCurve growMovementCurve;
	[SerializeField]
	private float growDistance = 5;
	[SerializeField]
	private float growDuration = 0.5f;

	[SerializeField]
	private AnimationCurve growWiggleCurve;

	[Header("Pulling")]
	[SerializeField]
	private GameObject rootPullEffect;
	[SerializeField]
	private float pullAnimationDuration;
	[SerializeField]
	private AnimationCurve pullRotationCurve;
	[SerializeField]
	private AnimationCurve pullXCurve;
	[SerializeField]
	private AnimationCurve pullYCurve;
	[SerializeField]
	private AnimationCurve pullAlphaCurve;
	[SerializeField]
	private Rect pullMovementArea;
	[SerializeField]
	private float pullMinRotationDegrees = -45;
	[SerializeField]
	private float pullMaxRotationDegrees = 45;
	[SerializeField]
	private float growDelay = 0;

	public async Task<RootInputBase> SpawnRoot(RemovableRoot rootPrefab)
	{
		// await Task.Delay((int)(this.spawnDelaySeconds * 1000));
		var root = Object.Instantiate(rootPrefab);
		root.transform.position = this.transform.position;
		var targetPosition = this.transform.position;
		Debug.Log("Root Appear started!");
		await root.Animate(this.growDuration, t =>
		{
			root.transform.position = targetPosition - new Vector3(0, this.growDistance * (1 - growMovementCurve.Evaluate(t)), 0);
			root.transform.rotation = Quaternion.Euler(0, 0, growWiggleCurve.Evaluate(t));
		});
		Debug.Log("Root Spawned!");
		this.currentRoot = root;
		Debug.Assert(root.possibleInputs != null && root.possibleInputs.Length > 0, "Root: not enough possible inputs!", root);
		var requiredInputs = root.possibleInputs.GetRandom();
		return requiredInputs;
	}

	public async Task RemoveRoot()
	{
		Debug.Log("Remove?");
		if (this.currentRoot)
		{
			Debug.Log("Remove!");
			// ignore this task
			_ = this.AnimatePull(this.currentRoot);
			this.currentRoot = null;
			//await Task.Delay((int)(this.growDelay * 1000));
		}
	}
	private async Task AnimatePull(RemovableRoot root)
	{
		var basePosition = (Vector2)root.transform.position;
		Object.Instantiate(rootPullEffect, basePosition, Quaternion.identity);
		var targetPosition = basePosition + pullMovementArea.RandomPoint();
		var negated = Random.Range(0, 2) == 0;
		var targetRotationDegrees = Random.Range(this.pullMinRotationDegrees, this.pullMaxRotationDegrees) * (negated ? -1 : 1);
		await root.Animate(this.pullAnimationDuration, t =>
		{
			var tx = this.pullXCurve.Evaluate(t);
			var ty = this.pullXCurve.Evaluate(t);
			root.SetAlpha(pullAlphaCurve.Evaluate(t));
			root.transform.position = new Vector2(basePosition.x * (1 - tx), basePosition.y * (1 - ty)) + new Vector2(targetPosition.x * tx, targetPosition.y * ty);
			root.transform.rotation = Quaternion.Euler(0, 0, targetRotationDegrees * this.pullRotationCurve.Evaluate(t));
		});
		Object.Destroy(root.gameObject);
	}

	protected override void InitSectionGameplay()
	{

	}

	protected override void CleanupSectionGameplay()
	{
		_ = this.RemoveRoot();
	}

	protected override void UpdateSectionGameplay()
	{

	}
}
