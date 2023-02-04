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
			// TODO(rw): implement based on camera
			return new Rect(0, 0, 1, 1);
		}
	}

	// TODO(rw): trigger if the viewport changes
	public event System.Action<Rect> ViewportChanged;
}
