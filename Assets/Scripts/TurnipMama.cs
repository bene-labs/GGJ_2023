using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TurnipMama : MonoBehaviour
{
    public float minSpawnSpeed = 0.5f;
    public float maxSpawnSpeed = 5.0f;

    public int pointMalus = 500;
    
    private bool _isAppearing;

    private float _dangerLevel = 0.0f;
    private bool isAngry = false;

    public GameObject jumpScarePrefab;
    
    private GameObject playerOne;
    private GameObject playerTwo;
    
    Dictionary<InputActions, bool> playerOneInputs = new() {
        {InputActions.Neutral, false},
        {InputActions.Down, false},
        {InputActions.Up, false},
        {InputActions.Decline, false},
    }; 
    Dictionary<InputActions, bool> playerTwoInputs = new() {
        {InputActions.Neutral, false},
        {InputActions.Down, false},
        {InputActions.Up, false},
        {InputActions.Decline, false},
    };
    
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

        isAngry = true;
        Debug.Log("Turnip Anger activated!");
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerOne = GameObject.Find("Player1Gameplay");
        playerTwo = GameObject.Find("Player2Gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAppearing)
        {
            DangerLevel = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime / _anim.GetCurrentAnimatorStateInfo(0).length;
            //print(DangerLevel);
        } 
        else if (this.isAngry)
        {
            HandlePlayerInputs();
        }
    }

    void HandlePlayerInputs()
    {
        playerOneInputs[InputActions.Neutral] = Input.GetButton(InputActions.Neutral.ToInputName(0));
        playerOneInputs[InputActions.Down] = Input.GetButton(InputActions.Down.ToInputName(0));
        playerOneInputs[InputActions.Up] = Input.GetButton(InputActions.Up.ToInputName(0));
        playerOneInputs[InputActions.Decline] = Input.GetButton(InputActions.Decline.ToInputName(0));

        if (playerOneInputs.ContainsValue(true))
        {
            Attack(playerOne);
        }
        
        playerTwoInputs[InputActions.Neutral] = Input.GetButton(InputActions.Neutral.ToInputName(1));
        playerTwoInputs[InputActions.Down] = Input.GetButton(InputActions.Down.ToInputName(1));
        playerTwoInputs[InputActions.Up] = Input.GetButton(InputActions.Up.ToInputName(1));
        playerTwoInputs[InputActions.Decline] = Input.GetButton(InputActions.Decline.ToInputName(1));
        
        if (playerTwoInputs.ContainsValue(true))
        {
            Attack(playerTwo);
        }
        
        if (playerOneInputs.ContainsValue(true) || playerTwoInputs.ContainsValue(true))
        {
            Instantiate(jumpScarePrefab);
            Destroy(this.gameObject);
        }
    }
    
    void Attack(GameObject player)
    {
        Debug.Log("Attacked Player: " + player.name);
        player.GetComponent<PlayerGameplay>().score -= this.pointMalus;
    }
    
    private void Disappear()
    {
        print("BYE World!");
        Destroy(this.gameObject);    
    }
    
    public void PeekOut(HidingSpot hidingSpot)
    {
        _anim.speed = Random.Range(minSpawnSpeed, maxSpawnSpeed);
        print("Turnip Spawned!");
        transform.position = hidingSpot.transform.position;
        gameObject.transform.rotation = hidingSpot.transform.rotation;
        _isAppearing = true;
        //_anim.Play("TurnipMamaAppear");
    }
}
