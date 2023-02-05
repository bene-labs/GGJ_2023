using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameplaySection : MonoBehaviour
{
	private bool isActiveGameplay = false;

	public void InitGameplay()
	{
		this.isActiveGameplay = true;
		this.InitSectionGameplay();
	}
	abstract protected void InitSectionGameplay();

	void Update()
	{
		if (this.isActiveGameplay)
		{
			this.UpdateSectionGameplay();
		}
	}

	abstract protected void UpdateSectionGameplay();

	public void CleanupGameplay()
	{
		this.isActiveGameplay = false;
		this.CleanupSectionGameplay();
	}
	abstract protected void CleanupSectionGameplay();
}

public static class IGameplaySectionExtensions
{
	public static void InitGameplayAll(this IGameplaySection[] sections)
	{
		if (sections != null)
		{
			foreach (var section in sections)
			{
				section.InitGameplay();
			}
		}
	}
	public static void CleanupGameplayAll(this IGameplaySection[] sections)
	{
		if (sections != null)
		{
			foreach (var section in sections)
			{
				section.CleanupGameplay();
			}
		}
	}
}
