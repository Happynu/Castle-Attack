using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Team teamA;
    public Team teamB;

    public Team currentTeam;

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
}
