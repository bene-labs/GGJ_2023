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
        this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerOne.score > playerTwo.score ? "Player1 wins!" : "Player2 wins!";
        
        Destroy(playerOne);
        Destroy(playerTwo);
        
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
