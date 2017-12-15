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
    private int goalNumber;
    private int numberOfBricks;
    private List<int> brickNumbers;

    [Space(10)]
    private Algorithm algorithm = new Algorithm();
    private BrickManager brickManager;

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
        goalNumber = Random.Range(20, 50); //Temp
        teamBlue.goalNumber = goalNumber;
        teamRed.goalNumber = goalNumber;
        EndNumber.text = goalNumber.ToString();

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

        //Generating bricks
        goalNumber = Random.Range(20, 50); //Temp
        numberOfBricks = 5;
        brickNumbers = algorithm.GenerateBrickNumbers(goalNumber, numberOfBricks);
        brickManager.SpawnBricks(brickNumbers);

        foreach (int item in brickNumbers)
        {
            Debug.Log(item);
        }

        teamBlue.equationText.text = "";
        teamRed.equationText.text = "";
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

    public void SpawnNewNumberBrick(Vector2 oldPosition, int number)
    {
        brickNumbers.Remove(number);
        brickManager.RemoveBrick(oldPosition);
        int newNumber = algorithm.GenerateNewBrick(currentTeam.currentNumber, brickNumbers);
        brickManager.SpawnNewBrick(newNumber);
        brickNumbers.Add(newNumber);
    }
}
