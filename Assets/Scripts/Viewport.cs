using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Viewport : MonoBehaviour
{
	[SerializeField]
	private float transitionDuration = 0;

	public Rect WorldSpaceDimensions
	{
		get
		{
			var screenSize = new Vector2(Screen.width, Screen.height);
			var relativeRect = this.camera.rect;
			var pixelsPerUnit = Screen.height / this.lastOrthographicSize * relativeRect.height;
			var unitsPerPixel = this.lastOrthographicSize / (Screen.height * relativeRect.height);
			var pixelsRect = new Rect(relativeRect.x * Screen.width, relativeRect.y * Screen.width, relativeRect.width * Screen.width, relativeRect.height * Screen.width);
			var unitsRect = new Rect(pixelsRect.x * unitsPerPixel, pixelsRect.y * unitsPerPixel, pixelsRect.width * unitsPerPixel, pixelsRect.height * unitsPerPixel);
			var xFactor = Screen.height / pixelsPerUnit;
			var yFactor = Screen.width / pixelsPerUnit;
			var worldSpaceOffset = ((Vector2)this.transform.position) - unitsRect.center;
			return new Rect(worldSpaceOffset, unitsRect.size);
		}
	}

	public event System.Action<Rect> ViewportChanged;

	private new Camera camera;

	private Rect lastRelativeRect;
	private Vector2 lastResolution;
	private float lastOrthographicSize;

	private float? currentXTarget = null;
	private float? transitionStartX = null;
	private float? transitionStartTime = null;

	public bool IsScrolling => this.currentXTarget != null;

	void Awake()
	{
		this.camera = this.GetComponent<Camera>();
		CheckChanges(triggerEvent: false);
	}

	void Update()
	{
		this.CheckChanges();
		if (this.IsScrolling)
		{
			var t = Mathf.Clamp01((Time.time - this.transitionStartTime.Value) / this.transitionDuration);
			this.transform.SetXPosition(this.currentXTarget.Value * t + this.transitionStartX.Value * (1 - t));
			if (t >= 1)
			{
				this.currentXTarget = null;
				this.transitionStartX = null;
				this.transitionStartTime = null;
			}
		}
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

	public void CenterOn(Rect worldPosition)
	{
		var offset = worldPosition.x - this.WorldSpaceDimensions.center.x;

		this.currentXTarget = this.transform.position.x + offset;
		this.transitionStartX = this.transform.position.x;
		this.transitionStartTime = Time.time;
	}
}
