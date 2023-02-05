using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnipMamaSpawner : IGameplaySection
{
	public TurnipMama turnipMama;
	public List<HidingSpot> hidingSpots;

	public float minSpawnDelay = 10.0f;
	public float maxSpawnDelay = 30.0f;
	private float _spawnDelay = 1.0f;
	private float _spawnTimer = 0.0f;

	// Start is called before the first frame update
	protected override void InitSectionGameplay()
	{
		this._spawnTimer = 0;
		_spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
	}

	// Update is called once per frame
	protected override void UpdateSectionGameplay()
	{
		_spawnTimer += Time.deltaTime;

		if (_spawnTimer >= _spawnDelay)
		{
			SpawnMama();
			_spawnTimer = 0.0f;
			_spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
		}
	}

	private void SpawnMama()
	{
		Debug.Assert(this.turnipMama != null, "prefab is null");
		var new_mama = Instantiate(turnipMama);
		new_mama.PeekOut(GetRandomHidingSpot());
	}

	private HidingSpot GetRandomHidingSpot()
	{
		var totalChance = hidingSpots.Sum(spot => spot.priority);

		var randKey = Random.Range(0, totalChance);
		var randIndex = 0;

		foreach (HidingSpot spot in hidingSpots)
		{
			randIndex += spot.priority;
			if (randIndex >= randKey)
			{
				return spot;
			}
		}
		return null;
	}

	protected override void CleanupSectionGameplay()
	{
	}
}
