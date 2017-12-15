using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Multiplier
{
    PLUS,
    MINUS,
    MULTIPLY,
    NONE
}

public class Team : MonoBehaviour
{
    public string color;
    public int goalNumber;

    private bool won = false;
    private bool started = false;

    private int currentNumber;
    private Multiplier currentMultiplier = Multiplier.NONE;

    /// <summary>
    /// Called when this team has hit a brick
    /// </summary>
    /// <param name="brick">the brick you hit.</param>
    /// <returns>Whether the move was allowed or not.</returns>
    public bool HitBrick(Interactable brick)
    {
        if (started == false)
        {
            if (brick is NumberBlock)
            {
                NumberBlock num = brick as NumberBlock;

                currentNumber = num.number;
                started = true;
                Debug.Log("Team " + color + " hit number: " + num.number);
                return true;
            }
            else return false;
        }

        //During the rest of the game, we have to check which brick was hit.
        else
        {
            //A Number block
            if (brick is NumberBlock)
            {
                NumberBlock num = brick as NumberBlock;

                //Only if there is a multiplier selected, you are allowed to hit a number.
                if (currentMultiplier != Multiplier.NONE)
                {
                    Calculate(num);
                    Debug.Log("Team " + color + " hit number: " + num.number);
                    return true;
                }
                else return false;
            }
            //A Plus block
            else if (brick is PlusBlock)
            {
                //Only if there is currently no multiplier you are allowed to hit one.
                if (currentMultiplier == Multiplier.NONE)
                {
                    currentMultiplier = Multiplier.PLUS;
                    Debug.Log("Team " + color + " hit multiplier: plus");
                    return true;
                }
                else return false;
            }
            //A Minus block
            else if (brick is MinusBlock)
            {
                //Only if there is currently no multiplier you are allowed to hit one.
                if (currentMultiplier == Multiplier.NONE)
                {
                    currentMultiplier = Multiplier.MINUS;
                    Debug.Log("Team " + color + " hit multiplier: minus");
                    return true;
                }
                else return false;
            }
            //A Multiply block
            else if (brick is MultiplyBlock)
            {
                //Only if there is currently no multiplier you are allowed to hit one.
                if (currentMultiplier == Multiplier.NONE)
                {
                    currentMultiplier = Multiplier.MULTIPLY;
                    Debug.Log("Team " + color + " hit multiplier: multiply");
                    return true;
                }
                else return false;
            }
        }
            return false;
    }

    void Calculate(NumberBlock num)
    {
        switch (currentMultiplier)
        {
            case Multiplier.PLUS:
                currentNumber = currentNumber + num.number;
                break;
            case Multiplier.MINUS:
                currentNumber = currentNumber - num.number;
                break;
            case Multiplier.MULTIPLY:
                currentNumber = currentNumber * num.number;
                break;
        }
        currentMultiplier = Multiplier.NONE;
        CheckWin();

        Debug.Log("currentNumber: " + currentNumber);
    }

    void CheckWin()
    {
        if (currentNumber == goalNumber)
        {
            won = true;
        }
    }
        
}
