using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
	[SerializeField]
	private RectTransform goodImage;
	[SerializeField]
	private RectTransform lowerBadImage;
	[SerializeField]
	private RectTransform indicator;
	[SerializeField]
	private float minScale;


	public void Apply(float min, float max, float t, InputActions requiredInput)
	{
		this.goodImage.localScale = new Vector3(max, max, max);
		this.lowerBadImage.localScale = new Vector3(min, min, min);
		this.indicator.localScale = new Vector3(t, t, t);
	}
}
