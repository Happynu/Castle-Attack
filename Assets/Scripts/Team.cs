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

    public int number1;
    public Multiplier operation;
    public int number2;
    public int result;
    public int process;

    public int score; 
    //1 = Need to get First number
    //2 = Need to get multiplier
    //3 = Need to get Second number
    //4 = Need to calculate

    public bool won = false;
    public bool started = false;
    public bool operationRound = false;

    UIManager UI;

    // Use this for initialization 
    void Start()
    {
        process = 1;
        InitializeTeam();
    }

    void CheckWin()
    {
        if (result == goalNumber)
        {
            score++;
            if (score == 3)
            {
                won = true;
            }
            GameManager.instance.EndRound();
        }
    }

    public void HitNumberBrick(Interactable i, bool start = false)
    {
        NumberBlock b = i as NumberBlock;
        if (start)
        {
            started = true;
            number1 = b.number;
            operationRound = true;

            process = 2;
        }
        else
        {
            number2 = b.number;
            process = 4;
            operationRound = true;
        }
    }

    public void HitOperationBrick(Interactable i)
    {
        OperationBlock b = i as OperationBlock;
        if (operation != Multiplier.NONE)
        {
            number1 = result;
            number2 = Int32.MinValue;
            result = Int32.MinValue;
        }

        operation = b.multiplier;
        process = 3;
        operationRound = false;
    }

    public void resetEquation()
    {
        number1 = result;
        operation = Multiplier.NONE;
        number2 = Int32.MaxValue;
        result = Int32.MaxValue;

        process = 2;
    }

    public void Calculate()
    { 
        switch (operation)
        {
            case Multiplier.PLUS:
                result = number1 + number2;
                break;
            case Multiplier.MINUS:
                result = number1 - number2;
                break;
            case Multiplier.MULTIPLY:
                result = number1 * number2;
                break;
            default:
                throw new NotImplementedException();
        }

        CheckWin();
    }

    public void InitializeTeam()
    {
        number1 = Int32.MinValue;
        operation = Multiplier.NONE;
        number2 = Int32.MinValue;
        result = Int32.MinValue;
        won = false;
        started = false;
        operationRound = false;
    }
}