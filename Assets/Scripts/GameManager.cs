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
    public Text EndNumberA;
    public Text EndNumberB;

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
        teamA.goalNumber = Random.Range(10, 20);
        EndNumberA.text = teamA.goalNumber.ToString();

        teamB.goalNumber = Random.Range(10, 20);
        EndNumberB.text = teamB.goalNumber.ToString();

        EquationA.text = "";
        EquationB.text = "";

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
    }
}
