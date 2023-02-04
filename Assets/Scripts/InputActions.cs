using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputActions
{
	/// <summary>
	/// Face Button down (A), or tap
	/// </summary>
	Neutral,
	/// <summary>
	/// Face Button left (X), or swipe down
	/// </summary>
	Down,
	/// <summary>
	/// Face Button up (Y), or swipe up
	/// </summary>
	Up,
	/// <summary>
	/// Face Button right (B), or swipe left
	/// </summary>
	Decline,
}

public static class InputActionsExtensions
{
	public static string ToInputName(this InputActions inputAction) => inputAction switch
	{
		InputActions.Neutral => "NeutralAction",
		InputActions.Down => "DownAction",
		InputActions.Up => "UpAction",
		InputActions.Decline => "DeclineAction",
		_ => null,
	};
}
