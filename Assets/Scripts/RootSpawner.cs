using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
	private float spawnDelaySeconds = 0.05f;
	private float despawnDelaySeconds = 0.05f;
	private RemovableRoot currentRoot;

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
		Object.Destroy(this.currentRoot.gameObject);
		await Task.Delay((int)(this.despawnDelaySeconds * 1000));
	}
}
