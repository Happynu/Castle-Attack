﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Space(10)]
    public Team teamBlue;
    public Team teamRed;

    [HideInInspector]
    public Team currentTeam;

    [Space(10)]
    public Text EndNumber;

    void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
	}

    void Start()
    {
        if (currentTeam == null)
        {
            currentTeam = teamBlue;
        }

        StartGame();
    }

    void Update ()
    {
		
	}

    public bool HitBrick(Interactable brick)
    {
        bool succes = currentTeam.HitBrick(brick);
        if (succes)
        {
            SwitchTeam();
        }
        return succes;
    }

    void SwitchTeam()
    {
        if (currentTeam == teamBlue)
        {
            currentTeam = teamRed;
        }
        else
        {
            currentTeam = teamBlue;
        }

        Debug.Log("TEAM " + currentTeam.color + "'s TURN");
    }

    void StartGame()
    {  
        int goal = Random.Range(10, 20);
        teamBlue.goalNumber = goal;
        teamRed.goalNumber = goal;
        EndNumber.text = goal.ToString();


        //randomly select starting team
        int startteam = Random.Range(0, 1);

        if (startteam == 0)
        {
            currentTeam = teamBlue;
        }
        else
        {
            currentTeam = teamRed;
        }
    }
}
