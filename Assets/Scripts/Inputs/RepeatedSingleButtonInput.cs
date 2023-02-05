using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "roots/repeated button input")]
public class RepeatedSingleButtonInput : RootInputBase
{
	[SerializeField]
	private int minInputCount;
	[SerializeField]
	private int maxInputCount;

	private InputActions requiredInput;
	private int requiredInputCount;
	private int currentInputCount;

	public override bool IsRepeatedInput => true;

	public override int RemainingInputCount => this.requiredInputCount - this.currentInputCount;

	public override InputActions? NextRequiredInput => this.requiredInput;

	public override int getScoreValue()
	{
		return requiredInputCount * 2;
	}
	
	protected override void Initialize()
	{
		this.requiredInput = typeof(InputActions).GetRandomValue<InputActions>();
		this.currentInputCount = 0;
		this.requiredInputCount = Random.Range(this.minInputCount, this.maxInputCount + 1);
	}

	public override bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out bool updatePrompts, out bool isCorrectInput)
	{
		var pressedButtons = inputs.Count(pair => pair.Value);
		if (inputs[this.requiredInput])
		{
			isCorrectInput = true;
			this.currentInputCount += 1;
			updatePrompts = true;
		}
		else if (pressedButtons > 0)
		{
			isCorrectInput = false;
			this.currentInputCount = 0;
			updatePrompts = true;
		}
		else
		{
			updatePrompts = false;
			isCorrectInput = false;
		}
		progress = this.currentInputCount * 1.0f / this.requiredInputCount;
		return this.currentInputCount >= this.requiredInputCount;
	}

	public override List<InputActions> GetInputPrompts()
	{
		return new List<InputActions>() { this.requiredInput };
	}
}
