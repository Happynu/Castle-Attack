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

    private int currentNumber;
    private Multiplier currentMultiplier = Multiplier.NONE;

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
                Debug.Log("Team " + color + " hit multiplier: plus");
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
                Debug.Log("Team " + color + " hit multiplier: minus");
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
                Debug.Log("Team " + color + " hit multiplier: multiply");
                return true;
            }
            else return false;
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
