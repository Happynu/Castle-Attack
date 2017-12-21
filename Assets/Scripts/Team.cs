using System;
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

    public Text number1;
    public Text operation;
    public Text number2;
    public Text equals;
    public Text result;

    private bool won = false;
    private bool started = false;

    public int currentNumber;
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
                started = true;
                UpdateUI(num);
                return true;
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
                if (currentMultiplier != Multiplier.NONE)
                {
                    UpdateUI(num);
                    return true;
                }
            }
            //An operation block
            else if (brick is OperationBlock)
            {
                OperationBlock num = brick as OperationBlock;
                //Only if there is currently no multiplier you are allowed to hit one.
                if (currentMultiplier == Multiplier.NONE)
                {
                    currentMultiplier = num.multiplier;
                    UpdateUI(num);
                    return true;
                }
            }
        }

        return false;
    }

    void CheckWin()
    {
        if (currentNumber == goalNumber)
        {
            won = true;
        }
    }

    public void ClearUI()
    {
        number1.text = "";
        operation.text = "";
        number2.text = "";
        equals.text = "";
        result.text = "";
    }

    public void UpdateUI(Interactable block)
    {
        if (number1.text == "")
        {
            Debug.Log("number 1 is leeg");
            if (block is NumberBlock)
            {
                Debug.Log("gecast naar numberblock");
                NumberBlock num = block as NumberBlock;
                number1.text = num.text.text;
            }
        }
        else if (operation.text == "")
        {
            if (block is NumberBlock)
            {
                OperationBlock num = block as OperationBlock;
                operation.text = num.text.text;
            }
        }
        else if (number2.text == "")
        {
            NumberBlock num = block as NumberBlock;
            number2.text = num.text.text;
        }
        else
        {
            Calculate();
        }
    }

    void Calculate()
    {
        equals.text = "=";

        int _number1;
        int _number2;

        Int32.TryParse(number1.text, out _number1);
        Int32.TryParse(number2.text, out _number2);

        int _result;

        switch (currentMultiplier)
        {
            case Multiplier.PLUS:
                _result = _number1 + _number2;
                break;
            case Multiplier.MINUS:
                _result = _number1 - _number2;
                break;
            case Multiplier.MULTIPLY:
                _result = _number1 * _number2;
                break;

            default:
                throw new NotImplementedException();
        }

        result.text = _result.ToString();

        currentMultiplier = Multiplier.NONE;
        currentNumber = _result;
    }
}