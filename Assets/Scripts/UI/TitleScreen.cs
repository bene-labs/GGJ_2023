using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : IGameplaySection
{
	[SerializeField]
	private GameProgression gameProgression;

	protected override void CleanupSectionGameplay()
	{
		this.gameObject.SetActive(false);
	}

	protected override void InitSectionGameplay()
	{
		this.gameObject.SetActive(true);
	}

	protected override void UpdateSectionGameplay()
	{
		if (Input.anyKeyDown)
		{
			this.gameProgression.Advance();
		}
	}
}
