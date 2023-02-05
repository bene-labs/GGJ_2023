using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedTurnip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Disappear()
    {
        print("Get goofed!");
        Destroy(this.gameObject);    
    }
}
