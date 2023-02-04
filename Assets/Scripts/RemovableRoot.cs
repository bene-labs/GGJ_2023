using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableRoot : MonoBehaviour
{
	[field: SerializeField]
	public Rect Dimensions { get; private set; }

	public InputActions[] displayedActions;
	[field: SerializeField]
	public bool IsGood;
}
