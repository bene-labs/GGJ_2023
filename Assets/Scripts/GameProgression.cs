using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgression : MonoBehaviour
{
	private static bool isFirstStart;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		isFirstStart = true;
	}

	[SerializeField]
	private IGameplaySection[] titleElements;
	[SerializeField]
	private IGameplaySection[] preGameElements;
	[SerializeField]
	private IGameplaySection[] gameElements;
	[SerializeField]
	private IGameplaySection[] postGameElements;

	private State? state;

	private IGameplaySection[] currentElements => this.state switch
	{
		State.Title => this.titleElements,
		State.PreGame => this.preGameElements,
		State.Game => this.gameElements,
		State.PostGame => this.postGameElements,
		_ => null,
	};


	void Start()
	{
		if (!isFirstStart)
		{
			this.state = State.Title;
		}
		isFirstStart = false;
		this.Advance();
	}

	public void Advance()
	{
		this.currentElements.CleanupGameplayAll();
		var nextState = this.state.Next();
		if (nextState != null)
		{
			this.state = nextState.Value;
			this.currentElements.InitGameplayAll();
		}
		else
		{
			SceneManager.LoadScene(0, LoadSceneMode.Single);
			Debug.Log("TODO Restart");
		}
		if (this.currentElements != null && this.currentElements.Length == 0)
		{
			this.Advance();
		}
	}

	public enum State
	{
		Title,
		PreGame,
		Game,
		PostGame,
	}
}

public static class GameProgressionStateExtensions
{
	public static GameProgression.State? Next(this GameProgression.State? state) => state == null ? GameProgression.State.Title : state.Value.Next();
	public static GameProgression.State? Next(this GameProgression.State state) => state switch
	{
		GameProgression.State.Title => GameProgression.State.PreGame,
		GameProgression.State.PreGame => GameProgression.State.Game,
		GameProgression.State.Game => GameProgression.State.PostGame,
		GameProgression.State.PostGame => null,
		_ => GameProgression.State.Title,
	};
}
