using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurnipMama : MonoBehaviour
{
    private bool _isAppearing;

    private float _dangerLevel = 0.0f;
    public float DangerLevel
    {
        get => _dangerLevel;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _dangerLevel = value;
        }
    }

    private Animator _anim;
    
    void UnleashAnger()
    {
        _isAppearing = false;
        DangerLevel = 100.0f;
        
        Debug.Log("Turnip Anger activated!");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAppearing)
        {
            DangerLevel = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime / _anim.GetCurrentAnimatorStateInfo(0).length;
            print(DangerLevel);
        }    
    }

    private void Disappear()
    {
        print("BYE World!");
        Destroy(this.gameObject);    
    }
    
    public void PeekOut(HidingSpot hidingSpot)
    {
        print("Turnip Spawned!");
        transform.position = hidingSpot.transform.position;
        gameObject.transform.rotation = hidingSpot.transform.rotation;
        _isAppearing = true;
        //_anim.Play("TurnipMamaAppear");
    }
}
