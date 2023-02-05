using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ScriptedAnimation : MonoBehaviour
{
	private System.Action<float> callback;
	private float duration;
	private float startTime;
	private TaskCompletionSource<bool> taskCompletionSource;
	private CancellationToken cancelToken;

	public Task Initalize(float duration, System.Action<float> callback, CancellationToken cancelToken)
	{
		this.startTime = Time.time;
		this.callback = callback;
		this.duration = duration;
		this.cancelToken = cancelToken;
		this.taskCompletionSource = new TaskCompletionSource<bool>();
		callback(0);
		return this.taskCompletionSource.Task;
	}

	void Update()
	{
		var t = Mathf.Clamp01((Time.time - this.startTime) / duration);
		this.callback(t);
		if (t >= 1)
		{
			this.taskCompletionSource.SetResult(true);
			Object.Destroy(this);
		}
		else if (this.cancelToken.IsCancellationRequested)
		{
			this.taskCompletionSource.SetResult(false);
			Object.Destroy(this);
		}
	}
}

public static class ScriptedAnimationExtensions
{
	public static Task Animate<T>(this T component, float duration, System.Action<float> callback, CancellationToken cancelToken = default) where T : Component
	{
		return component.gameObject.AddComponent<ScriptedAnimation>().Initalize(duration, callback, cancelToken);
	}
}
