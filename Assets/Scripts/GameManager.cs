using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager ui;

    [Space(10)]
    public Team teamBlue;
    public Team teamRed;

    [Space(10)]
    public EndRoundScreenManager endRoundScreenManager;

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

    public IEnumerator RoundTimout()
    {
        GameManager.instance.timedout = true;
        yield return new WaitForSeconds(4f);
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
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();

        StartGame();
    }

    public void EndRound()
    {
        endRoundScreenManager.gameObject.SetActive(true);
        endRoundScreenManager.StartEndRoundScreen(teamRed.score, teamBlue.score);
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
        ChangeRoundType();

        Debug.Log("TEAM " + currentTeam.color + "'s TURN");
    }

    void StartGame()
    {
        goalNumber = Random.Range(10, 25); //Temp
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

        ui.StartUI();

        //Generating bricks
        goalNumber = Random.Range(20, 50); //Temp
        numberOfBricks = 5;
        brickNumbers = algorithm.GenerateBrickNumbers(goalNumber, numberOfBricks);
        brickManager.SpawnBricks(brickNumbers);
        brickManager.StartNumberRound();
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
        int newNumber = algorithm.GenerateNewBrick(currentTeam.result, brickNumbers);
        brickManager.SpawnNewBrick(newNumber);
        brickNumbers.Add(newNumber);
        ChangeRoundType();
    }

    private void ChangeRoundType()
    {
        if (currentTeam.operationRound)
        {
            brickManager.StartOperationRound();
        }
        else
        {
            brickManager.StartNumberRound();
        }
    }

    /// <summary>
    /// Called when this team has hit a brick
    /// </summary>
    /// <param name="brick">the brick you hit.</param>
    /// <returns>Whether the move was allowed or not.</returns>

    public bool HitBrick(Interactable brick)
    {
        Team t = currentTeam;

        //At the start of the game, pick a start number.
        if (t.started == false)
        {
            if (brick is NumberBlock)
            {
                NumberBlock num = brick as NumberBlock;
                t.started = true;
                t.number1 = num.number;
                t.operationRound = true;
            }
            else
            {
                return false;
            }
        }

        //During the rest of the game, we have to check which brick was hit.
        else
        {
            //A Number block
            if (brick is NumberBlock)
            {
                NumberBlock num = brick as NumberBlock;

                //Only if there is a multiplier selected, you are allowed to hit a number.
                if (!t.operationRound)
                {
                    t.number2 = num.number;
                    t.Calculate();
                    t.operationRound = true;

                    ui.UpdateUI(currentTeam);
                }
                else
                {
                    return false;
                }
            }
            //An operation block
            else if (brick is OperationBlock)
            {
                OperationBlock num = brick as OperationBlock;
                //Only if there is currently no multiplier you are allowed to hit one.
                if (t.operationRound)
                {
                    if(t.operation != Multiplier.NONE)
                    {
                        t.number1 = t.result;
                        t.number2 = -1;
                        t.result = -1;
                    }
                    t.operation = num.multiplier;
                    t.operationRound = false;
                }
                else
                {
                    return false;
                }
            }
        }

        ui.RemoveBrick(brick, currentTeam);
        ui.UpdateUI(currentTeam);
        SwitchTeam();

        return true;
    }
}
