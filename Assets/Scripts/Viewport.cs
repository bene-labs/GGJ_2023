using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Viewport : MonoBehaviour
{
	public Rect WorldSpaceDimensions
	{
		get
		{
			var relativeRect = this.GetComponent<Camera>().rect;
			var xFactor = this.lastOrthographicSize / Screen.height * Screen.width;
			var yFactor = this.lastOrthographicSize;
			return new Rect(relativeRect.x * xFactor, relativeRect.y * yFactor, relativeRect.width * xFactor, relativeRect.height * yFactor);
		}
	}

	public event System.Action<Rect> ViewportChanged;

	private Camera camera;

	private Rect lastRelativeRect;
	private Vector2 lastResolution;
	private float lastOrthographicSize;

	void Awake()
	{
		this.camera = this.GetComponent<Camera>();
		CheckChanges(triggerEvent: false);
	}

	private void CheckChanges(bool triggerEvent = true)
	{
		var relativeRect = this.camera.rect;
		var resolution = new Vector2(Screen.width, Screen.height);
		var orthographicSize = this.camera.orthographicSize * 2;
		var changed = relativeRect != this.lastRelativeRect || resolution != this.lastResolution || orthographicSize != this.lastOrthographicSize;
		this.lastRelativeRect = relativeRect;
		this.lastResolution = resolution;
		this.lastOrthographicSize = orthographicSize;
		if (changed && triggerEvent)
		{
			this.ViewportChanged?.Invoke(this.WorldSpaceDimensions);
		}
	}

	void Update()
	{
		this.CheckChanges();
	}
}
