using System.Collections;
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
    public Image Edge;

    //For hitting bricks too many times, don't touch
    public bool timedout = false;

    public IEnumerator HitTimout()
    {
        GameManager.instance.timedout = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.timedout = false;
    }

    public void StartHitTimout()
    {
        StartCoroutine(HitTimout());
    }

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
            ChangeEdgeColor("Red");
        }
        else
        {
            currentTeam = teamBlue;
            ChangeEdgeColor("Blue");
        }

        Debug.Log("TEAM " + currentTeam.color + "'s TURN");
    }

    void StartGame()
    {  
        int goal = Random.Range(10, 20);
        teamBlue.goalNumber = goal;
        teamRed.goalNumber = goal;
        EndNumber.text = goal.ToString();

        teamBlue.equationText.text = "";
        teamRed.equationText.text = "";


        //randomly select starting team
        int startteam = Random.Range(0, 2);

        if (startteam == 0)
        {
            currentTeam = teamBlue;
            ChangeEdgeColor("Blue");
        }
        else
        {
            currentTeam = teamRed;
            ChangeEdgeColor("Red");
        }
    }

    void ChangeEdgeColor(string color)
    {
        Color myColor = new Color();

        if (color == "Blue")
        {
            ColorUtility.TryParseHtmlString("#2A7CCDFF", out myColor);
        }
        else if (color == "Red")
        {
            ColorUtility.TryParseHtmlString("#CD2A2AFF", out myColor);
        }
        else //Set color to white
        {
            ColorUtility.TryParseHtmlString("#FFFFFFFF", out myColor);
        }

        Edge.color = myColor;
    }
}
