using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public PlayerGameplay playerOne;
    public PlayerGameplay playerTwo;

    
    [Tooltip("Measured in Minutes")] 
    public float timeLimit = 3.0f; 
    private float _timeCounter = 0.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _timeCounter += Time.deltaTime;

        if (_timeCounter / 60 > timeLimit)
        {
            DisplayVictoryScreen();
        }
    }

    private void DisplayVictoryScreen()
    {
        if (playerOne.score > playerTwo.score)
            this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Congrats Player1...";
        else if  (playerOne.score <  playerTwo.score)
            this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Congrats Player2...";
        else
            this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "It's a Tie!";

        Destroy(playerOne);
        Destroy(playerTwo);
        Destroy(GameObject.Find("TurnipMamaSpawner"));
        
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
