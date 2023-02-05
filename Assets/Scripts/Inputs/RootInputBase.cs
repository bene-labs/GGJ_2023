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

	abstract public int getScoreValue();
	
	abstract public List<InputActions> GetInputPrompts();
	abstract public InputActions? NextRequiredInput { get; }
	abstract public bool IsRepeatedInput { get; }
	abstract public int RemainingInputCount { get; }

	/// <summary>
	/// Checks the current input state, updates the internal state accordinly, and reports <c>true</c> if the input sequence is done.
	/// </summary>
	/// <param name="inputs"></param>
	/// <param name="progress"></param>
	/// <param name="buttonPrompt"></param>
	/// <returns></returns>
	abstract public bool HandleInputs(Dictionary<InputActions, bool> inputs, out float? progress, out bool updatePrompts, out bool isCorrectInput);
}
