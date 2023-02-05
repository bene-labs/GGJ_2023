using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerGameplay : IGameplaySection
{
	[SerializeField]
	private Viewport viewport;
	[SerializeField]
	private RootSpawner spawner;
	[SerializeField]
	private RemovableRoot[] availableRoots;
	[SerializeField]
	private InputPrompt inputPromptPrefab;
	private List<InputPrompt> inputPrompts = new();
	[SerializeField]
	private InputCount inputCount;
	[SerializeField]
	private RectTransform inputPromptParent;
	[SerializeField]
	private int playerIndex;

	[Header("UI Fading")]
	[SerializeField]
	private CanvasGroup fadedElement;
	[SerializeField]
	private float fadeOutDuration;
	[SerializeField]
	private AnimationCurve fadeOutCurve;
	[SerializeField]
	private float fadeInDuration;
	[SerializeField]
	private AnimationCurve fadeInCurve;

	private bool isActiveGameplay;

	private Task<RootInputBase> currentRootTask = null;
	private RootInputBase currentInput = null;
	private Task removeTask;

	public int score = 0;

	private Task uiFadeTask;

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
		this.inputCount.gameObject.SetActive(false);
		this.UpdatePrompt();
	}

	protected override void InitSectionGameplay()
	{
		this.spawner.InitGameplay();
		this.inputCount.gameObject.SetActive(false);
		this.UpdatePrompt();
		this.isActiveGameplay = true;
	}

	protected override void CleanupSectionGameplay()
	{
		// TODO(rw): cleanup
		this.spawner.CleanupGameplay();
	}

	override protected void UpdateSectionGameplay()
	{
		inputs[InputActions.Neutral] = Input.GetButtonDown(InputActions.Neutral.ToInputName(this.playerIndex));
		inputs[InputActions.Down] = Input.GetButtonDown(InputActions.Down.ToInputName(this.playerIndex));
		inputs[InputActions.Up] = Input.GetButtonDown(InputActions.Up.ToInputName(this.playerIndex));
		inputs[InputActions.Decline] = Input.GetButtonDown(InputActions.Decline.ToInputName(this.playerIndex));

		if (this.currentRootTask == null)
		{
			this.currentRootTask = this.spawner.SpawnRoot(this.availableRoots.GetRandom());
		}
		if (this.currentRootTask != null && this.currentRootTask.IsCompleted)
		{
			if (this.currentInput == null)
			{
				this.currentInput = this.currentRootTask.Result.CopyForUse();
				this.UpdatePrompt();
			}
			if (this.currentInput != null)
			{
				if (this.removeTask == null)
				{
					if (this.currentInput.HandleInputs(this.inputs, out var progress, out var updateInputPrompt))
					{
						score += this.currentInput.getScoreValue();
						Debug.Log("Points got: " + this.currentInput.getScoreValue().ToString());
						this.removeTask = this.spawner.RemoveRoot();
						this.UpdatePrompt();
					}
					else
					{
						if (updateInputPrompt)
						{
							this.UpdatePrompt();
						}
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

	private void UpdatePrompt()
	{
		if (this.currentInput != null && this.removeTask == null)
		{
			this.inputPrompts.SetAllGameObjectsActive(true);
			if (this.currentInput.IsRepeatedInput)
			{
				this.inputCount.gameObject.SetActive(true);
				this.inputCount.ApplyCount(this.currentInput.RemainingInputCount);
				if (this.inputPrompts.Count < 1)
				{
					this.InstantiatePrompt();
				}
				while (this.inputPrompts.Count > 1)
				{
					this.DestroyAndRemoveInputPromptByIndex(1);
				}
				this.inputPrompts[0].ApplyInput(this.currentInput.NextRequiredInput.Value);
			}
			else
			{
				this.inputCount.gameObject.SetActive(false);
				var prompts = this.currentInput.GetInputPrompts();
				while (this.inputPrompts.Count < prompts.Count)
				{
					this.InstantiatePrompt();
				}
				while (this.inputPrompts.Count > prompts.Count)
				{
					this.DestroyAndRemoveInputPromptByIndex(prompts.Count);
				}

				for (int index = 0; index < prompts.Count; index += 1)
				{
					this.inputPrompts[index].ApplyInput(prompts[index], done: index < prompts.Count - this.currentInput.RemainingInputCount);
				}
			}
		}
		else
		{
			if (this.currentInput != null && this.removeTask != null)
			{
				if (this.fadedElement.alpha > 0 && this.uiFadeTask == null)
				{
					this.uiFadeTask = this.FadeOutUI();
				}
			}
		}
	}
	private async Task FadeOutUI()
	{
		if (this.inputCount.gameObject.activeInHierarchy)
		{
			this.inputCount.ApplyCount(0);
		}
		foreach (var prompt in this.inputPrompts)
		{
			prompt.ApplyInput(done: true);
		}
		await this.fadedElement.Animate(this.fadeOutDuration, t =>
		{
			this.fadedElement.alpha = 1 - this.fadeOutCurve.Evaluate(t);
		});
		this.inputCount.gameObject.SetActive(false);
		this.inputPrompts.SetAllGameObjectsActive(false);
		this.uiFadeTask = null;
		this.fadedElement.alpha = 1;
	}
	private InputPrompt InstantiatePrompt()
	{
		var inputPrompt = Object.Instantiate(this.inputPromptPrefab);
		inputPrompt.transform.SetParent(inputPromptParent, worldPositionStays: false);
		this.inputPrompts.Add(inputPrompt);
		return inputPrompt;
	}
	private void DestroyAndRemoveInputPromptByIndex(int index)
	{
		var prompt = this.inputPrompts[index];
		Debug.Assert(prompt != null);
		this.inputPrompts.Remove(prompt);
		prompt.DestroyGameObject();
	}
}
