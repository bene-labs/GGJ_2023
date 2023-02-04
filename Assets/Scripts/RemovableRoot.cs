using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableRoot : MonoBehaviour
{
	[field: SerializeField]
	public Rect Dimensions { get; private set; }

	[field: SerializeField]
	public RootInputBase[] possibleInputs { get; private set; }

	public Rect WorldPosition => Dimensions.Offsetted(this.transform.position);

	public InputActions[] displayedActions;
	[field: SerializeField]
	public bool IsGood;
}
