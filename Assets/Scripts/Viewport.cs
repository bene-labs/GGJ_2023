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

	public void CenterOn(Rect worldPosition)
	{
		var offset = worldPosition.x - this.WorldSpaceDimensions.center.x;
		this.transform.position += new Vector3(offset, 0, 0);
	}

	void Update()
	{
		this.CheckChanges();
	}
}
