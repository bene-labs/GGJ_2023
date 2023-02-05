using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "roots/Circle Input")]
public class CircleHitInput : RootInputBase
{
	public override InputActions? NextRequiredInput => this.requiredInput;

	public override bool IsRepeatedInput => false;

	public override int RemainingInputCount => 1;

	public override bool UseCircularIndicator => true;

	[field: SerializeField]
	public float minTime { get; private set; } = 0.4f;
	[field: SerializeField]
	public float maxTime { get; private set; } = 0.6f;
	[SerializeField]
	private AnimationCurve timeMovement;
	[SerializeField]
	private float cycleDuration = 2;
	[SerializeField]
	private int score = 25;


	public InputActions requiredInput { get; private set; }
	private List<InputActions> requiredInputList = new();

	private float currentT = 0;
	public float currentTime { get; private set; }

	public override List<InputActions> GetInputPrompts()
	{
		return this.requiredInputList;
	}

	public override int getScoreValue()
	{
		return this.score;
	}

	protected override void Initialize()
	{
		this.currentT = 0;
		this.requiredInput = typeof(InputActions).GetRandomValue<InputActions>();
		this.requiredInputList.Add(this.requiredInput);
	}


	public override bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out bool updatePrompts)
	{
		this.currentT += (Time.deltaTime / this.cycleDuration) % 1;
		this.currentTime = this.timeMovement.Evaluate(this.currentT);
		progress = this.currentTime;
		var isPressed = inputs[this.requiredInput];
		updatePrompts = true;
		if (isPressed)
		{
			if (this.currentTime >= this.minTime && this.currentTime < this.maxTime)
			{
				return true;
			}
		}
		return false;
	}
}
