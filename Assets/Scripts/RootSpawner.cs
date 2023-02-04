using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
	[SerializeField]
	private float spawnDelaySeconds = 0.05f;
	[SerializeField]
	private float despawnDelaySeconds = 0.05f;
	private RemovableRoot currentRoot;

	[SerializeField]
	private GameObject rootPullEffect;

	public async Task<RootInputBase> SpawnRoot(RemovableRoot rootPrefab)
	{
		await Task.Delay((int)(this.spawnDelaySeconds * 1000));
		var root = Object.Instantiate(rootPrefab);
		this.currentRoot = root;
		root.transform.position = this.transform.position;
		Debug.Assert(root.possibleInputs != null && root.possibleInputs.Length > 0, "Root: not enough possible inputs!", root);
		var requiredInputs = root.possibleInputs.GetRandom();
		return requiredInputs;
	}

	public async Task RemoveRoot()
	{
		Object.Instantiate(rootPullEffect, this.currentRoot.transform.position, Quaternion.identity);
		Object.Destroy(this.currentRoot.gameObject);
		await Task.Delay((int)(this.despawnDelaySeconds * 1000));
	}
}
