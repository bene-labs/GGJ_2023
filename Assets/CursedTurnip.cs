using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedTurnip : MonoBehaviour
{
    private GameObject ui;

    public AudioClip sound;
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("GameUI");
        ui.SetActive(false);
        
        AudioSource.PlayClipAtPoint(sound, this.transform.position, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Disappear()
    {
        ui.SetActive(true);
        Destroy(this.gameObject);    
    }
}
