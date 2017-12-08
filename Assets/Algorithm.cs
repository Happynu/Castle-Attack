using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    private List<int> blockNumbers = new List<int> { 3,6,7 };
    private List<int> possibleAnswers = new List<int>();
    private int currentNumber = 8;
    private int goalNumber = 16;

    // Use this for initialization
    void Start()
    {
        if (checkPossibility())
        {
            Debug.Log("Goal number possible: " + goalNumber);
        }
        else
        {
            Debug.Log("Goal number not possible: " + goalNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool checkPossibility()
    {
        for (int i1 = 0; i1 < blockNumbers.Count; i1++)
        {
            int answerPlus = currentNumber + blockNumbers[i1];
            if (!possibleAnswers.Contains(answerPlus))
            {
                possibleAnswers.Add(answerPlus);
            }
            int answerMinus = currentNumber - blockNumbers[i1];
            if (!possibleAnswers.Contains(answerMinus) && answerMinus > 0)
            {
                possibleAnswers.Add(answerMinus);
            }
            int answerMultiply = currentNumber * blockNumbers[i1];
            if (!possibleAnswers.Contains(answerMultiply))
            {
                possibleAnswers.Add(answerMultiply);
            }
        }

        possibleAnswers.Sort();

        foreach (int i in possibleAnswers)
        {
            Debug.Log(i);
        }

        if (possibleAnswers.Contains(goalNumber))
        {
            return true;
        }
        return false;
    }
}

