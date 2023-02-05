using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PlayerScore : MonoBehaviour
{
    public PlayerGameplay player;

    public int score = 0;

    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.score != score)
        {
            score = player.score;
            text.SetText(score.ToString());
        }
    }
}
