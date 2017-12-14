using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    private List<int> blockNumbers = new List<int> { 2, 5 ,8};
    private List<int> possibleAnswers = new List<int>();
    private int currentNumber = 0;
    private int goalNumber = 402;
    private int nextNewBrick = 0;

    private int minBrickValue = 1;
    private int maxBrickValue = 10;

    void Start()
    {
        createNewBrick();
        foreach (int i in possibleAnswers)
        {
            Debug.Log(i);
        }
    }

    //This method will calculate the value for a new brick number
    private void createNewBrick()
    {
        nextNewBrick = 0;
        List<int> possibleBricks = new List<int>();
        if (checkPossibility())
        {
            nextNewBrick = Random.Range(minBrickValue, maxBrickValue);
            Debug.Log("Random brick: " + nextNewBrick);
        }
        else
        {
            for (int i = 1; i < ((maxBrickValue - minBrickValue) + 2); i++)
            {
                if (!blockNumbers.Contains(i))
                {
                    if (checkPossibility(i))
                    {
                        possibleBricks.Add(i);
                    }
                }
            }
            if (possibleBricks.Count == 0)
            {
                nextNewBrick = Random.Range(minBrickValue, maxBrickValue);
                Debug.Log("No possible brick, random: " + nextNewBrick);
            }
            else
            {
                nextNewBrick = possibleBricks[Random.Range(0, possibleBricks.Count)];
                Debug.Log("New possible brick: " + nextNewBrick);
            }
        }
    }

    private bool checkPossibility(int checkNumber = 0)
    {
        int number = currentNumber;
        List<int> possibleAnswersList = new List<int>();
        List<int> blockNumbersList = new List<int>();
        blockNumbersList.AddRange(blockNumbers);
        if (checkNumber != 0) blockNumbersList.Add(checkNumber);
        for (int i1 = 0; i1 < blockNumbersList.Count - 1; i1++)
        {
            for (int i2 = 0; i2 < blockNumbersList.Count - 1; i2++)
            {
                for (int i3 = 0; i3 < 3; i3++)
                {
                    calculate(number, i1, i3, blockNumbersList, possibleAnswersList);
                }
                int removeNumber = blockNumbersList[0];
                blockNumbersList.Remove(removeNumber);
                blockNumbersList.Add(removeNumber);
            }
        }
        if (checkNumber == 0) possibleAnswers = possibleAnswersList;
        possibleAnswersList.Sort();
        foreach (int i in possibleAnswersList)
        {
            if (i == goalNumber) return true;
        }
        return false;
    }

    private void calculate(int number, int index, int operation, List<int> blockNumbersList, List<int> possibleAnswersList)
    {
        int newNumber = number;
        switch (operation)
        {
            case 0:
                int answerPlus = number + blockNumbersList[index];
                if (!possibleAnswersList.Contains(answerPlus) && answerPlus > 0)
                {
                    possibleAnswersList.Add(answerPlus);
                }
                newNumber = answerPlus;
                break;
            case 1:
                int answerMinus = number - blockNumbersList[index];
                if (!possibleAnswersList.Contains(answerMinus) && answerMinus > 0)
                {
                    possibleAnswersList.Add(answerMinus);
                }
                newNumber = answerMinus;
                break;
            case 2:
                int answerMultiply = number * blockNumbersList[index];

                if (!possibleAnswersList.Contains(answerMultiply) && answerMultiply > 0)
                {
                    possibleAnswersList.Add(answerMultiply);
                }
                newNumber = answerMultiply;
                break;
        }
        int newIndex = index + 1;
        if (index < blockNumbersList.Count - 1 && newNumber < (maxBrickValue * maxBrickValue))
        {
            for (int i = 0; i < 3; i++)
            {
                calculate(newNumber, newIndex, i, blockNumbersList, possibleAnswersList);
            }
        }
    }
}

