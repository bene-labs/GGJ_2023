using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputCount : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;

	public void ApplyCount(int count)
	{
		this.text.text = count.ToString();
	}
}
