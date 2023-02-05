using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Countdown : MonoBehaviour
{
	[SerializeField]
	private bool autoStart;

	[Header("Numbers")]
	[SerializeField]
	private CanvasGroup[] numberUiElements;
	[SerializeField]
	private float numberAnimationDuration;
	[SerializeField]
	private AnimationCurve numberAlphaCurve;
	[SerializeField]
	private AnimationCurve numberRotationDegreeCurve;
	[SerializeField]
	private AnimationCurve numberScaleCurve;

	[Header("Go")]
	[SerializeField]
	private CanvasGroup goElement;
	[SerializeField]
	private float goAnimationDuration;
	[SerializeField]
	private AnimationCurve goAlphaCurve;
	[SerializeField]
	private AnimationCurve goRotationDegreeCurve;
	[SerializeField]
	private AnimationCurve goScaleCurve;


	void Start()
	{
		if (this.autoStart)
		{
			_ = this.PlayCowntdownAnimation();
		}
	}


	public async Task PlayCowntdownAnimation()
	{
		this.gameObject.SetActive(true);
		this.ApplyNumberState(0, 0);
		this.numberUiElements.SetAllGameObjectsActive(false);
		this.goElement.gameObject.SetActive(false);
		this.numberUiElements[0].gameObject.SetActive(true);
		Debug.LogFormat("3");
		await this.Animate(this.numberAnimationDuration, t => this.ApplyNumberState(0, t));
		this.numberUiElements[0].gameObject.SetActive(false);
		this.numberUiElements[1].gameObject.SetActive(true);
		Debug.LogFormat("2");
		await this.Animate(this.numberAnimationDuration, t => this.ApplyNumberState(1, t));
		this.numberUiElements[1].gameObject.SetActive(false);
		this.numberUiElements[2].gameObject.SetActive(true);
		Debug.LogFormat("1");
		await this.Animate(this.numberAnimationDuration, t => this.ApplyNumberState(2, t));
		this.numberUiElements[2].gameObject.SetActive(false);
		this.goElement.gameObject.SetActive(true);
		Debug.LogFormat("Go");
		await this.Animate(this.numberAnimationDuration, this.ApplyGoState);
		this.goElement.gameObject.SetActive(false);
		this.gameObject.SetActive(false);
	}

	private void ApplyNumberState(int index, float t)
	{
		var element = this.numberUiElements[index];
		element.alpha = this.numberAlphaCurve.Evaluate(t);
		var scale = this.numberScaleCurve.Evaluate(t);
		element.transform.localScale = new Vector3(scale, scale, scale);
		element.transform.rotation = Quaternion.Euler(0, 0, this.numberRotationDegreeCurve.Evaluate(t));
	}
	private void ApplyGoState(float t)
	{
		var element = this.goElement;
		element.alpha = this.goAlphaCurve.Evaluate(t);
		var scale = this.goScaleCurve.Evaluate(t);
		element.transform.localScale = new Vector3(scale, scale, scale);
		element.transform.rotation = Quaternion.Euler(0, 0, this.goRotationDegreeCurve.Evaluate(t));
	}
}
