using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPrompt : MonoBehaviour
{
	public void ApplyInput(InputActions input)
	{
		var rotationDegrees = input switch
		{
			InputActions.Neutral => 180,
			InputActions.Down => 90,
			InputActions.Up => 0,
			InputActions.Decline => -90,
			_ => 0,
		};
		this.transform.rotation = Quaternion.Euler(0, 0, rotationDegrees);
	}
}
