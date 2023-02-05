using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
#if !UNITY_EDITOR
			Application.Quit();
#else
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
#endif
}
