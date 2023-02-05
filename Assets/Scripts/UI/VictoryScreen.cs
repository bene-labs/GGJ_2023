using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : IGameplaySection
{
	[SerializeField]
	private TextMeshProUGUI textElement;
	[SerializeField]
	public PlayerGameplay playerOne;
	[SerializeField]
	public PlayerGameplay playerTwo;
	[SerializeField]
	private GameProgression gameProgression;

	[Header("Repeat Button")]
	[SerializeField]
	private Button repeatButton;
	[SerializeField]
	private CanvasGroup repeatButtonCanvas;
	[SerializeField]
	private float buttonTransitionDelay;
	[SerializeField]
	private float buttonTransitionDuration;
	[SerializeField]
	private float buttonInitialYOffset = -512;
	[SerializeField]
	private AnimationCurve buttonMovementCurve;
	[SerializeField]
	private AnimationCurve buttonAlphaCurve;

	private bool isTransitionDone;

	void Start()
	{
		this.repeatButton.onClick.AddListener(this.HandleRepeateButtonClick);
	}

	protected override void CleanupSectionGameplay()
	{
		this.gameObject.SetActive(false);
		this.textElement.gameObject.SetActive(false);
	}

	protected override void InitSectionGameplay()
	{
		this.isTransitionDone = false;
		this.gameObject.SetActive(true);
		if (playerOne.score > playerTwo.score)
			this.textElement.text = "Congrats Player1...";
		else if (playerOne.score < playerTwo.score)
			this.textElement.text = "Congrats Player2...";
		else
			this.textElement.text = "It's a Tie!";

		this.textElement.gameObject.SetActive(true);

		this.TransitionButton();
	}
	private async void TransitionButton()
	{
		this.repeatButton.gameObject.SetActive(false);
		var rectTransform = this.repeatButtonCanvas.GetComponent<RectTransform>();
		await Task.Delay((int)(this.buttonTransitionDelay * 1000));
		this.repeatButton.gameObject.SetActive(true);
		await this.Animate(this.buttonTransitionDuration, t =>
		{
			this.repeatButtonCanvas.alpha = this.buttonAlphaCurve.Evaluate(t);
			var offsetT = this.buttonMovementCurve.Evaluate(t);
			rectTransform.SetAnchoredYPosition((1 - offsetT) * this.buttonInitialYOffset);
		});
		this.isTransitionDone = true;
	}

	private void HandleRepeateButtonClick()
	{
		this.TryAdvance();
	}

	protected override void UpdateSectionGameplay()
	{
		if (Input.anyKey)
		{
			this.TryAdvance();
		}
	}

	private void TryAdvance()
	{
		if (this.isTransitionDone)
		{
			this.gameProgression.Advance();
		}
	}
}
