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

    [HideInInspector]
    public Team currentTeam;

    [Space(10)]
    public Text EndNumber;

    [Space(10)]
    private int goalNumber;
    private int numberOfBricks;
    private List<int> brickNumbers;

    [Space(10)]
    public Algorithm algorithm;
    private BrickManager brickManager;

    [Space(10)]
    public Image win;
    public Text textRed;
    public Text textBlue;

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
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();

        StartGame();
    }

    public void EndRound()
    {
        win.gameObject.SetActive(true);
        if (teamBlue.score == 1)
        {
            textBlue.gameObject.SetActive(true);
        } else
        {
            textRed.gameObject.SetActive(true);
        }
        
    }

    public void SwitchTeam()
    {
        ui.UpdateUI(currentTeam);

        if (currentTeam == teamBlue)
        {
            currentTeam = teamRed;
        }
        else
        {
            currentTeam = teamBlue;
        }

        ui.ChangeEdgeColor(currentTeam);
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
        }
        else
        {
            currentTeam = teamRed;
        }

        ui.StartUI();
        ui.ChangeEdgeColor(currentTeam);

        //Generating bricks
        goalNumber = Random.Range(20, 50); //Temp
        numberOfBricks = 5;
        brickNumbers = algorithm.GenerateBrickNumbers(goalNumber, numberOfBricks);
        brickManager.SpawnBricks(brickNumbers);
        brickManager.StartNumberRound();
    }

    //Spawns a new number brick, based om the algorithm
    public void SpawnNewNumberBrick(Vector2 oldPosition, int number)
    {
        brickNumbers.Remove(number);
        brickManager.RemoveBrick(oldPosition);
        int newNumber = algorithm.GenerateNewBrick(currentTeam.result, brickNumbers);
        brickManager.SpawnNewBrick(newNumber);
        brickNumbers.Add(newNumber);
        ChangeRoundType();
    }

    //Changes the currentteam's round type, so they can either hit number or operation bricks
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
    /// Called when a team has hit a brick
    /// </summary>
    /// <param name="brick">the brick you hit.</param>
    /// <returns>Whether the move was allowed or not.</returns>
    public bool HitBrick(Interactable brick)
    {
        //At the start of the game, pick a start number.
        if (currentTeam.started == false)
        {
            if (brick is NumberBlock)
            {
                ui.StartMoveNumberBrick(brick, currentTeam);
                currentTeam.HitNumberBrick(brick, true);
                currentTeam.started = true;
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
                if (!currentTeam.operationRound)
                {
                    ui.StartMoveNumberBrick(brick, currentTeam);
                    currentTeam.HitNumberBrick(num);
                }
                else
                {
                    return false;
                }
            }
            //An operation block
            else if (brick is OperationBlock)
            {
                
                //Only if there is currently no multiplier you are allowed to hit one.
                if (currentTeam.operationRound)
                {
                    ui.StartMoveOperationBrick(brick, currentTeam);
                    currentTeam.HitOperationBrick(brick);
                }
                else
                {
                    return false;
                }
            }
        }

        StartHitTimout();

        return true;
    }
}
