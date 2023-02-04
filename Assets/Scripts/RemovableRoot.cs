using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableRoot : MonoBehaviour
{
	[field: SerializeField]
	public Rect Dimensions { get; private set; }

	[field: SerializeField]
	public RootInputBase[] possibleInputs { get; private set; }

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	public Rect WorldPosition => this.Dimensions.Offsetted(this.transform.position);

	public InputActions[] displayedActions;
	[field: SerializeField]
	public bool IsGood;

	public void SetAlpha(float alpha)
	{
		this.spriteRenderer.color = Color.white.WithAlpha(alpha);
	}
}
