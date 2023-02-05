using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class LoopedAnimation : IGameplaySection
{
	private CancellationTokenSource cancelSource;

	[SerializeField]
	private float animationDuration;
	[SerializeField]
	private AnimationCurve xCurve;
	[SerializeField]
	private AnimationCurve yCurve;
	[SerializeField]
	private AnimationCurve rotationCurve;
	[SerializeField]
	private float positionFactor = 800;

	protected override void CleanupSectionGameplay()
	{
		if (this.cancelSource != null)
		{
			this.cancelSource.Cancel();
			this.cancelSource = null;
		}
	}

	protected override void InitSectionGameplay()
	{
		this.cancelSource = new CancellationTokenSource();
		this.PlayAnimation();
	}

	private async void PlayAnimation()
	{
		while (!this.cancelSource.IsCancellationRequested)
		{
			await this.Animate(this.animationDuration, t =>
			{
				var position = new Vector2(this.xCurve.Evaluate(t), this.yCurve.Evaluate(t)) * this.positionFactor;
				var rotation = this.rotationCurve.Evaluate(t);
				// Debug.LogFormat("position: {0}, rotation: {1}", position, rotation);
				(this.transform as RectTransform).anchoredPosition = position;
				this.transform.rotation = Quaternion.Euler(0, 0, rotation);
			}, this.cancelSource.Token);
		}
	}

	protected override void UpdateSectionGameplay()
	{
	}
}
