using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
public class PlayerScore : MonoBehaviour
{
    public PlayerGameplay player;
    
    public int score = 0;
    private int previousScore = 0;
    public float scoreAnimationSpeed = 1.0f;
    public float scoreCountdownDelay = 1.0f;
    private int addedPoints = 0;
    private int incomingPoints = 0;
    private int targetScore = 0;

    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }
        if (player.score != targetScore)
        {
            previousScore = score;
            targetScore = player.score;
            print("Incoming Points!");
            addedPoints = player.score - (score + incomingPoints);
            
            text.text = score + (addedPoints > 0 ? " +" : " -") + Mathf.Abs(addedPoints);

            if (addedPoints < 0)
                text.color = Color.red;
            else
                text.color = Color.green;
            if (incomingPoints == 0) 
                Invoke("AnimateIncomingPoints", scoreCountdownDelay);
        }
    }

    private async Task AnimateIncomingPoints()
    {
        await this.Animate(scoreAnimationSpeed, t =>
        {
            incomingPoints = (int) (addedPoints - addedPoints * t);
            score = (int) (previousScore + addedPoints * t);
            if (score < 0)
                score = 0;
            text.text = score + (addedPoints > 0 ? " +" : " -") + Mathf.Abs(incomingPoints);
        });
        incomingPoints = 0;
        addedPoints = 0;
        score = targetScore;
        if (score < 0)
            score = 0;
        text.color = Color.white;
        text.text = score.ToString();
    } 
}
