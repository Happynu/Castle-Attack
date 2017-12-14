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
    public bool won = false;

    public int currentNumber;
    public Multiplier currentMultiplier = Multiplier.NONE;

    public bool HitBrick(Interactable brick)
    {
        if (brick is NumberBlock)
        {
            NumberBlock num = brick as NumberBlock;
            if (currentMultiplier != Multiplier.NONE)
            {
                Calculate(num);
                Debug.Log("Team " + color + " hit number: " + num.number);
                return true;
            }
            else return false;
        }
        else if (brick is PlusBlock)
        {
            PlusBlock plus = brick as PlusBlock;
            if (currentMultiplier == Multiplier.NONE)
            {
                currentMultiplier = Multiplier.PLUS;
                return true;
            }
            else return false;
        }
        else if (brick is MinusBlock)
        {
            MinusBlock min = brick as MinusBlock;
            if (currentMultiplier == Multiplier.NONE)
            {
                currentMultiplier = Multiplier.MINUS;
                return true;
            }
            else return false;
        }
        else if (brick is MultiplyBlock)
        {
            MultiplyBlock mult = brick as MultiplyBlock;
            if (currentMultiplier == Multiplier.NONE)
            {
                currentMultiplier = Multiplier.MULTIPLY;
                return true;
            }
            else return false;
        }
        else return false;
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
