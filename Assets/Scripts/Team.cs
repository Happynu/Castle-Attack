using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text equationText;

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
                equationText.text = GetEquation();
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
                    equationText.text = GetEquation();
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
                    equationText.text = GetEquation();
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
                    equationText.text = GetEquation();
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
                    equationText.text = GetEquation();
                    return true;
                }
                else return false;
            }
        }
        return false;
    }

    void Calculate(NumberBlock num)
    {
        string text = GetEquation();

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

        text = text + " = " + currentNumber;
        equationText.text = text;
        currentMultiplier = Multiplier.NONE;
        CheckWin();
    }

    void CheckWin()
    {
        if (currentNumber == goalNumber)
        {
            won = true;
        }
    }

    public string GetEquation()
    {
        switch (currentMultiplier)
        {
            case Multiplier.NONE:
                return currentNumber.ToString();
            case Multiplier.PLUS:
                return currentNumber + " +";
            case Multiplier.MINUS:
                return currentNumber + " -";
            case Multiplier.MULTIPLY:
                return currentNumber + " x";
        }
        return "";
    }
}