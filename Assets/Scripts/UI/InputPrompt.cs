using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InputPrompt : MonoBehaviour
{
	[SerializeField]
	private Sprite doneSprite;
	private Sprite regularSprite;
	private Image imageElement;

	void Awake()
	{
		this.imageElement = this.GetComponent<Image>();
		this.regularSprite = imageElement.sprite;
	}

	public void ApplyInput(InputActions? input = null, bool done = false)
	{
		if (input != null)
		{
			var rotationDegrees = input.Value switch
			{
				InputActions.Neutral => 180,
				InputActions.Down => 90,
				InputActions.Up => 0,
				InputActions.Decline => -90,
				_ => 0,
			};
			this.transform.rotation = Quaternion.Euler(0, 0, rotationDegrees);
		}
		this.imageElement.sprite = done ? this.doneSprite : this.regularSprite;
	}
}
