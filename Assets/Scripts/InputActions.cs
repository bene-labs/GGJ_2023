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
	public static string ToInputName(this InputActions inputAction, int playerIndex)
	{
		return inputAction switch
		{
			InputActions.Neutral => "DownAction",
			InputActions.Down => "LeftAction",
			InputActions.Up => "UpAction",
			InputActions.Decline => "RightAction",
			_ => null,
		} + (playerIndex + 1);
	}
}
