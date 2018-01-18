using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickManager : MonoBehaviour
{
    public List<GameObject> bricks;
    private List<GameObject> activeBricks;
    private Vector2 lastLocation;

    void Start()
    {
        activeBricks = new List<GameObject>();
    }

    public void SpawnBricks(List<int> brickNumbers)
    {
        int randomIndex;
        GameObject go;
        OperationBlock operationBlock;
        NumberBlock numberBlock;

        randomIndex = Random.Range(0, bricks.Count - 1);
        go = bricks[randomIndex];
        go.AddComponent<OperationBlock>();
        operationBlock = go.GetComponent<OperationBlock>();
        operationBlock.text = go.transform.Find("Canvas").gameObject.transform.Find("Text").GetComponent<Text>();
        operationBlock.multiplier = Multiplier.PLUS;
        operationBlock.brick = go.GetComponent<Brick>();
        operationBlock.UpdateText();
        activeBricks.Add(go);

        while (activeBricks.Contains(go))
        {
            randomIndex = Random.Range(0, bricks.Count - 1);
            go = bricks[randomIndex];
        }
        go.AddComponent<OperationBlock>();
        operationBlock = go.GetComponent<OperationBlock>();
        operationBlock.text = go.transform.Find("Canvas").gameObject.transform.Find("Text").GetComponent<Text>();
        operationBlock.multiplier = Multiplier.MINUS;
        operationBlock.brick = go.GetComponent<Brick>();
        operationBlock.UpdateText();
        activeBricks.Add(go);

        while (activeBricks.Contains(go))
        {
            randomIndex = Random.Range(0, bricks.Count - 1);
            go = bricks[randomIndex];
        }
        go.AddComponent<OperationBlock>();
        operationBlock = go.GetComponent<OperationBlock>();
        operationBlock.text = go.transform.Find("Canvas").gameObject.transform.Find("Text").GetComponent<Text>();
        operationBlock.multiplier = Multiplier.MULTIPLY;
        operationBlock.brick = go.GetComponent<Brick>();
        operationBlock.UpdateText();
        activeBricks.Add(go);

        foreach (int i in brickNumbers)
        {
            while (activeBricks.Contains(go))
            {
                randomIndex = Random.Range(0, bricks.Count - 1);
                go = bricks[randomIndex];
            }
            go.AddComponent<NumberBlock>();
            numberBlock = go.GetComponent<NumberBlock>();
            numberBlock.text = go.transform.Find("Canvas").gameObject.transform.Find("Text").GetComponent<Text>();
            numberBlock.number = i;
            numberBlock.brick = go.GetComponent<Brick>();
            numberBlock.UpdateText();
            activeBricks.Add(go);
        }
    }

    public void RemoveBrick(Vector2 location)
    {
        GameObject numberObject = null;
        lastLocation = location;

        foreach (GameObject go in activeBricks)
        {
            if (go.GetComponent<NumberBlock>() != null)
            {
                if (go.transform.position.x == location.x && go.transform.position.y == location.y)
                {
                    numberObject = go;
                    break;
                }
            }

        }
        if (numberObject != null)
        {
            numberObject.GetComponentInChildren<Text>().text = "";
            NumberBlock numberBlock = numberObject.GetComponent<NumberBlock>();
            activeBricks.Remove(numberObject);
            Destroy(numberBlock);
        }
    }

    public void SpawnNewBrick(int number)
    {
        int randomIndex;
        GameObject go;
        NumberBlock numberBlock;

        randomIndex = Random.Range(0, bricks.Count - 1);
        go = bricks[randomIndex];

        while (activeBricks.Contains(go) || (go.transform.position.x == lastLocation.x && go.transform.position.y == lastLocation.y))
        {
            randomIndex = Random.Range(0, bricks.Count - 1);
            go = bricks[randomIndex];
        }

        go.AddComponent<NumberBlock>();
        numberBlock = go.GetComponent<NumberBlock>();
        numberBlock.text = go.transform.Find("Canvas").gameObject.transform.Find("Text").GetComponent<Text>();
        numberBlock.number = number;
        numberBlock.brick = go.GetComponent<Brick>();
        numberBlock.UpdateText();
        activeBricks.Add(go);
    }

    public void StartNumberRound()
    {
        foreach (GameObject go in activeBricks)
        {
            if (go.GetComponent<NumberBlock>() != null)
            {
                StartCoroutine(go.transform.Find("Canvas/Moss").GetComponent<MossTextureChanger>().HideTexture());
            }
            else
            {
                StartCoroutine(go.transform.Find("Canvas/Moss").GetComponent<MossTextureChanger>().ShowTexture());
            }
        }
    }

    public void StartOperationRound()
    {
        foreach (GameObject go in activeBricks)
        {
            if (go.GetComponent<NumberBlock>() != null)
            {
                StartCoroutine(go.transform.Find("Canvas/Moss").GetComponent<MossTextureChanger>().ShowTexture());
            }
            else
            {
                StartCoroutine(go.transform.Find("Canvas/Moss").GetComponent<MossTextureChanger>().HideTexture());
            }
        }
        foreach (GameObject go in bricks)
        {
            if (!activeBricks.Contains(go))
            {
                StartCoroutine(go.transform.Find("Canvas/Moss").GetComponent<MossTextureChanger>().HideTexture());
            }
        }
    }
}
