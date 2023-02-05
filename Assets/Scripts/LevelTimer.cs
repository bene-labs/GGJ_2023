using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : IGameplaySection
{
	public PlayerGameplay playerOne;
	public PlayerGameplay playerTwo;


	[Tooltip("Measured in Minutes")]
	public float timeLimit = 3.0f;
	private float _timeCounter = 0.0f;

	[SerializeField]
	private GameProgression gameProgression;

	private TextMeshProUGUI text;

	override protected void InitSectionGameplay()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	protected override void CleanupSectionGameplay()
	{
		text.enabled = false;
	}

	override protected void UpdateSectionGameplay()
	{
		_timeCounter += Time.deltaTime;

		text.text = "Time left: " + System.TimeSpan.FromMinutes((timeLimit - _timeCounter / 60)).ToString(@"m\:ss");
		if (_timeCounter / 60 > timeLimit)
		{
			this.gameProgression.Advance();
		}
	}
}
