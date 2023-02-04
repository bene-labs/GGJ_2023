using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScriptedAnimation : MonoBehaviour
{
	private System.Action<float> callback;
	private float duration;
	private float startTime;
	private TaskCompletionSource<bool> taskCompletionSource;

	public Task Initalize(float duration, System.Action<float> callback)
	{
		this.startTime = Time.time;
		this.callback = callback;
		this.duration = duration;
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
	}
}

public static class ScriptedAnimationExtensions
{
	public static Task Animate<T>(this T component, float duration, System.Action<float> callback) where T : Component
	{
		return component.gameObject.AddComponent<ScriptedAnimation>().Initalize(duration, callback);
	}
}
