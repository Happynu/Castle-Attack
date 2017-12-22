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

    public int score = 0;

    public bool won = false;
    public bool started = false;
    public bool operationRound = false;

    // Use this for initialization 
    void Start()
    {
        number1 = -1;
        operation = Multiplier.NONE;
        number2 = -1;
        result = -1;
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
        }
        else
        {
            number2 = b.number;
            Calculate();
            operationRound = true;
        }
    }

    public void HitOperationBrick(Interactable i)
    {
        OperationBlock b = i as OperationBlock;
        if (operation != Multiplier.NONE)
        {
            number1 = result;
            number2 = -1;
            result = -1;
        }

        operation = b.multiplier;
        operationRound = false;
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
}