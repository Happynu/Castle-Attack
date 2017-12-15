using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Space(10)]
    public Team teamA;
    public Team teamB;

    [Space(10)]
    public Team currentTeam;

    [Space(10)]
    public Text EndNumber;

    [Space(10)]
    public Text EquationA;
    public Text EquationB;

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
            currentTeam = teamA;
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
        if (currentTeam == teamA)
        {
            currentTeam = teamB;
        }
        else
        {
            currentTeam = teamA;
        }

        Debug.Log("TEAM " + currentTeam.color + "'s TURN");
    }

    void StartGame()
    {  
        int goal = Random.Range(10, 20);
        teamA.goalNumber = goal;
        teamB.goalNumber = goal;
        EndNumber.text = goal.ToString();

        //randomly select starting team
        int startteam = Random.Range(0, 1);

        if (startteam == 0)
        {
            currentTeam = teamA;
        }
        else
        {
            currentTeam = teamB;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        EquationA.text = teamA.GetEquation();
        EquationB.text = teamB.GetEquation();
    }
}
