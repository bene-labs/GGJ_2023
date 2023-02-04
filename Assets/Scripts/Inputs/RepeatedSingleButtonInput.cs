using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "roots/repeated button input")]//, fileName = nameof(RepeatedSingleButtonInput))]
public class RepeatedSingleButtonInput : RootInputBase
{
	[SerializeField]
	private int minInputCount;
	[SerializeField]
	private int maxInputCount;

	private InputActions requiredInput;
	private int requiredInputCount;
	private int currentInputCount;

	protected override void Initialize()
	{
		this.requiredInput = ((InputActions[])System.Enum.GetValues(typeof(InputActions))).GetRandom();
		this.currentInputCount = 0;
		this.requiredInputCount = Random.Range(this.minInputCount, this.maxInputCount + 1);
		Debug.LogFormat("{0} required {1} inputs", this.requiredInputCount, this.requiredInput);
	}

	public override bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out InputActions buttonPrompt)
	{
		progress = null;
		buttonPrompt = requiredInput;
		var pressedButtons = inputs.Count(pair => pair.Value);
		if (inputs[this.requiredInput])
		{
			this.currentInputCount += 1;
		}
		else if (pressedButtons > 0)
		{
			Debug.Log("Wrong Button Pressed!");
			this.currentInputCount = 0;
		}
		progress = this.currentInputCount * 1.0f / this.requiredInputCount;
		return this.currentInputCount >= this.requiredInputCount;
	}
}
