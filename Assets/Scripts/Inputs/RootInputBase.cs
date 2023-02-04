using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the base for input prompts to the user.
/// </summary>
public abstract class RootInputBase : ScriptableObject
{
	public RootInputBase CopyForUse()
	{
		var instance = Object.Instantiate(this);
		instance.Initialize();
		return instance;
	}
	public void ReleaseCopy()
	{
		Object.Destroy(this);
	}

	abstract protected void Initialize();

	/// <summary>
	/// Checks the current input state, updates the internal state accordinly, and reports <c>true</c> if the input sequence is done.
	/// </summary>
	/// <param name="inputs"></param>
	/// <param name="progress"></param>
	/// <param name="buttonPrompt"></param>
	/// <returns></returns>
	abstract public bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out InputActions buttonPrompt);
}
