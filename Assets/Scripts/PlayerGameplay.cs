using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
	[SerializeField]
	private Viewport viewport;
	[SerializeField]
	private RootSpawner spawner;
	[SerializeField]
	private RemovableRoot[] availableRoots;

	private Task<RootInputBase> currentRootTask = null;
	private RootInputBase currentInput = null;
	private Task removeTask;

	private Dictionary<InputActions, bool> inputs = new() {
		{InputActions.Neutral, false},
		{InputActions.Down, false},
		{InputActions.Up, false},
		{InputActions.Decline, false},
	};

	void Start()
	{
		Debug.Assert(this.viewport != null, "PlayerGameplay: no viewport assigned!", this);
		Debug.Assert(this.spawner != null, "PlayerGameplay: no PlayerObjectSpawner assigned!", this);
		Debug.Assert(this.availableRoots != null && availableRoots.Length > 0, "no roots assigned!", this);
	}

	void Update()
	{
		inputs[InputActions.Neutral] = Input.GetButtonDown(InputActions.Neutral.ToInputName());
		inputs[InputActions.Down] = Input.GetButtonDown(InputActions.Down.ToInputName());
		inputs[InputActions.Up] = Input.GetButtonDown(InputActions.Up.ToInputName());
		inputs[InputActions.Decline] = Input.GetButtonDown(InputActions.Decline.ToInputName());

		if (this.currentRootTask == null)
		{
			this.currentRootTask = this.spawner.SpawnRoot(this.availableRoots.GetRandom());
		}
		if (this.currentRootTask != null && this.currentRootTask.IsCompleted)
		{
			if (this.currentInput == null)
			{
				this.currentInput = this.currentRootTask.Result.CopyForUse();
			}
			if (this.currentInput != null)
			{
				if (this.removeTask == null)
				{
					if (this.currentInput.HandleInputs(this.inputs, out var progress, out var prompt))
					{
						this.removeTask = this.spawner.RemoveRoot();
					}
				}
				if (this.removeTask != null && this.removeTask.IsCompleted)
				{
					this.currentRootTask = null;
					this.currentInput.ReleaseCopy();
					this.currentInput = null;
					this.removeTask = null;
				}
			}
		}
	}
}
