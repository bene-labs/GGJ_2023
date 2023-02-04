using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "roots/button input sequence")]
public class InputSequence : RootInputBase
{
	[SerializeField]
	private int minInputCount;
	[SerializeField]
	private int maxInputCount;

	private List<InputActions> requiredInputs;
	private int correctInputs;

	public override InputActions? NextRequiredInput => this.requiredInputs.Count > this.correctInputs ? this.requiredInputs[this.correctInputs] : (InputActions?)null;
	public override bool IsRepeatedInput => false;
	public override int RemainingInputCount => this.requiredInputs.Count - this.correctInputs;

	public override List<InputActions> GetInputPrompts() => this.requiredInputs;

	public override int getScoreValue()
	{
		return requiredInputs.Count * 2 * requiredInputs.Count;
	}
	
	public override bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out bool updatePrompts)
	{
		progress = this.correctInputs * 1.0f / this.requiredInputs.Count;
		var optionalNextInput = this.NextRequiredInput;
		if (optionalNextInput != null)
		{
			var nextInput = optionalNextInput.Value;
			var pressedButtons = inputs.Count(pair => pair.Value);
			if (inputs[nextInput])
			{
				this.correctInputs += 1;
				updatePrompts = true;
				return this.RemainingInputCount == 0;
			}
			else if (pressedButtons > 0)
			{
				this.correctInputs = 0;
				updatePrompts = true;
				return false;
			}
			else
			{
				updatePrompts = false;
				return false;
			}
		}
		else
		{
			updatePrompts = true;
			return true;
		}
	}

	protected override void Initialize()
	{
		var count = Random.Range(this.minInputCount, this.maxInputCount);
		this.requiredInputs = new(count);

		for (int i = 0; i < count; i += 1)
		{
			this.requiredInputs.Add(typeof(InputActions).GetRandomValue<InputActions>());
		}
	}
}
