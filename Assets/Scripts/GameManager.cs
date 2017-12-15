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

    [Space(10)]
    private int goalNumber;
    private int numberOfBricks;
    private List<int> brickNumbers;

    [Space(10)]
    private Algorithm algorithm = new Algorithm();
    private BrickManager brickManager;

    void Awake()
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

        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();

        StartGame();
    }

    void Update()
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
       goalNumber = Random.Range(20, 50); //Temp
        //        teamBlue.goalNumber = goal;
        //   teamRed.goalNumber = goal;
        EndNumber.text = goalNumber.ToString();

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

        //Generating bricks
        goalNumber = Random.Range(20, 50); //Temp
        numberOfBricks = 5;
        brickNumbers = algorithm.GenerateBrickNumbers(goalNumber, numberOfBricks);
        brickManager.SpawnBricks(brickNumbers);

        foreach (int item in brickNumbers)
        {
            Debug.Log(item);
        }
    }
}
