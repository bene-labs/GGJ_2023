using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : IGameplaySection
{
	[SerializeField]
	private TextMeshPro textElement;
	[SerializeField]
	public PlayerGameplay playerOne;
	[SerializeField]
	public PlayerGameplay playerTwo;

	protected override void CleanupSectionGameplay()
	{
		this.gameObject.SetActive(false);
		this.textElement.gameObject.SetActive(false);
	}

	protected override void InitSectionGameplay()
	{
		this.gameObject.SetActive(true);
		if (playerOne.score > playerTwo.score)
			this.textElement.text = "Congrats Player1...";
		else if (playerOne.score < playerTwo.score)
			this.textElement.text = "Congrats Player2...";
		else
			this.textElement.text = "It's a Tie!";

		this.textElement.gameObject.SetActive(true);
	}

	protected override void UpdateSectionGameplay()
	{

	}
}
