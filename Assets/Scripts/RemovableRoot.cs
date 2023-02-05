using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RemovableRoot : MonoBehaviour
{
	[field: SerializeField]
	public Rect Dimensions { get; private set; }

	[field: SerializeField]
	public RootInputBase[] possibleInputs { get; private set; }

	[FormerlySerializedAs("spriteRenderers")] [SerializeField]
	private Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	public Rect WorldPosition => this.Dimensions.Offsetted(this.transform.position);

	public InputActions[] displayedActions;
	[field: SerializeField]
	public bool IsGood;

	public void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites.GetRandom();
	}

	public void SetAlpha(float alpha)
	{
		this.spriteRenderer.color = Color.white.WithAlpha(alpha);
	}
}
