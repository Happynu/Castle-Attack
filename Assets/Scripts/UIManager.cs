using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text number1Red;
    public Text operationRed;
    public Text number2Red;
    public Text equalsRed;
    public Text resultRed;

    [Space(10)]
    public Text number1Blue;
    public Text operationBlue;
    public Text number2Blue;
    public Text equalsBlue;
    public Text resultBlue;

    [Space(10)]
    public Text finalValue;
    public Image BlueteamBanner;
    public Image RedteamBanner;

    public Team Red;
    public Team Blue;

    public Image Edge;

    public GameObject teamFlags;

    public void StartUI(Team currentTeam)
    {
        number1Blue.text = "";
        number2Blue.text = "";
        operationBlue.text = "";
        resultBlue.text = "";

        number1Red.text = "";
        number2Red.text = "";
        operationRed.text = "";
        resultRed.text = "";
        SetFlagPosition(currentTeam);
    }


    public void UpdateUI(Team team)
    {
        switch (team.color)
        {
            case "blue":
                if (Blue.number1 != -1)
                {
                    number1Blue.text = Blue.number1.ToString();
                }
                else
                {
                    number1Blue.text = "";
                }

                operationBlue.text = ConvertMultiplier(Blue.operation);

                if (Blue.number2 != -1)
                {
                    number2Blue.text = Blue.number2.ToString();
                }
                else
                {
                    number2Blue.text = "";
                }

                if (Blue.result != -1)
                {
                    resultBlue.text = Blue.result.ToString();
                }
                else
                {
                    resultBlue.text = "";
                }
                break;

            case "red":
                if (Red.number1 != -1)
                {
                    number1Red.text = Red.number1.ToString();
                }
                else
                {
                    number1Red.text = "";
                }

                operationRed.text = ConvertMultiplier(Red.operation);

                if (Red.number2 != -1)
                {
                    number2Red.text = Red.number2.ToString();
                }
                else
                {
                    number2Red.text = "";
                }

                if (Red.result != -1)
                {
                    resultRed.text = Red.result.ToString();
                }
                else
                {
                    resultRed.text = "";
                }
                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }

        ChangeEdgeColor(team);
        StartCoroutine(SwitchTeamFlags(team));
        Debug.Log("edge changed");
    }

    public void ChangeEdgeColor(Team t)
    {
        Color myColor = new Color();

        if (t.color == "blue")
        {
            ColorUtility.TryParseHtmlString("#2A7CCDFF", out myColor);
        }

        else if (t.color == "red")
        {
            ColorUtility.TryParseHtmlString("#CD2A2AFF", out myColor);
        }
        else //Set color to white
        {
            ColorUtility.TryParseHtmlString("#FFFFFFFF", out myColor);
        }

        Edge.color = myColor;
    }

    public void StartMoveNumberBrick(Interactable brick, Team team)
    {
        StartCoroutine(MoveNumberBrick(brick, LabelPosition(team)));
    }

    public void StartMoveOperationBrick(Interactable brick, Team team)
    {
        StartCoroutine(MoveOperationBrick(brick, LabelPosition(team)));
    }

    Vector3 LabelPosition(Team team)
    {
        if (team.color == "blue")
        {
            switch (team.process)
            {
                //First number
                case 1:
                    return number1Blue.transform.position;

                //Operation
                case 2:
                    return operationBlue.transform.position;

                //Second number
                case 3:
                    return number2Blue.transform.position;

                //Calculate
                case 4:
                    throw new System.NotImplementedException("calculate not inplemented yet");
            }
        }
        else //if (team.color == "blue")
        {
            switch (team.process)
            {
                //First number
                case 1:
                    return number1Red.transform.position;

                //Operation
                case 2:
                    return operationRed.transform.position;

                //Second number
                case 3:
                    return number2Red.transform.position;

                //Calculate
                case 4:
                    throw new System.NotImplementedException("calculate not inplemented yet");
            }
        }

        throw new System.NotImplementedException();
    }

    private IEnumerator MoveNumberBrick(Interactable brick, Vector3 dest)
    {
        //detach label from brick
        Transform canvas = brick.transform.Find("Canvas");
        Transform canvasCopy = Instantiate(canvas, canvas.transform).transform;
        canvas.gameObject.GetComponentInChildren<Text>().text = "";

        float speed = 5f;

        float startDistance = Vector3.Distance(canvasCopy.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvasCopy.transform.position.x != dest.x && canvasCopy.transform.position.y != dest.y)
        {
            //Move
            canvasCopy.transform.position = Vector3.MoveTowards(canvasCopy.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvasCopy.transform.transform.position, dest);
            if (currentDistance > (startDistance / 0.3))
            {
                canvasCopy.transform.localScale += Vector3.one * Time.deltaTime * 1.01f;
            }
            else
            {
                canvasCopy.transform.localScale -= Vector3.one * Time.deltaTime * 1.01f;
            }

            yield return null;
        }

        //Reset label
        Destroy(canvasCopy.gameObject);

        //Next turn
        GameManager.instance.SpawnNewNumberBrick(new Vector2(brick.transform.position.x, brick.transform.position.y), (brick as NumberBlock).number);
        GameManager.instance.SwitchTeam();

    }

    private IEnumerator CalculateAnimation()
    {
        //make sum bigger and smaller (pulse), while fading in the equals symbol
        yield return null;
    }

    private IEnumerator MoveOperationBrick(Interactable brick, Vector3 dest)
    {
        Transform canvas = brick.transform.Find("Canvas");
        Transform canvasCopy = Instantiate(canvas, canvas.transform).transform;

        float speed = 5f;

        float startDistance = Vector3.Distance(canvasCopy.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvasCopy.transform.position.x != dest.x && canvasCopy.transform.position.y != dest.y)
        {
            //Move
            canvasCopy.transform.position = Vector3.MoveTowards(canvasCopy.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvasCopy.transform.position, dest);
            if (currentDistance > (startDistance / 0.3))
            {
                canvasCopy.transform.localScale += Vector3.one * Time.deltaTime * 1.01f;
            }
            else
            {
                canvasCopy.transform.localScale -= Vector3.one * Time.deltaTime * 1.01f;
            }

            yield return null;
        }

        //Destroy label copy
        Destroy(canvasCopy.gameObject);

        //Next turn
        GameManager.instance.SwitchTeam();
    }

    string ConvertMultiplier(Multiplier m)
    {
        switch (m)
        {
            case Multiplier.PLUS:
                return "+";
            case Multiplier.MINUS:
                return "-";
            case Multiplier.MULTIPLY:
                return "x";
            case Multiplier.NONE:
                return "";
            default:
                throw new System.NotImplementedException();
        }
    }

    public void SetFlagPosition(Team currentTeam)
    {
        Vector3 dest = teamFlags.transform.position;
        switch (currentTeam.color)
        {
            case "blue":
                dest.x = -0.4f;
                break;
            case "red":
                dest.x = 0.4f;
                break;
        }
        teamFlags.transform.position = dest;
    }

    public IEnumerator DestroyFlags()
    {
        GameObject blueFlag = teamFlags.transform.Find("Flag-Blue").gameObject;
        GameObject redFlag = teamFlags.transform.Find("Flag-Red").gameObject;
        Vector3 dest = teamFlags.transform.position;
        dest.x = 0;

        while (blueFlag.transform.position != dest && redFlag.transform.position != dest)
        {
            blueFlag.transform.position = Vector3.MoveTowards(blueFlag.transform.position, dest, Time.deltaTime * 1.0f);
            redFlag.transform.position = Vector3.MoveTowards(redFlag.transform.position, dest, Time.deltaTime * 1.0f);
            yield return null;
        }
        Destroy(teamFlags);
        yield return null;
    }

    public IEnumerator SwitchTeamFlags(Team currentTeam)
    {
        Vector3 dest = teamFlags.transform.position;
        switch (currentTeam.color)
        {
            case "blue":
                dest.x = 0.4f;
                break;
            case "red":
                dest.x = -0.4f;
                break;
        }

        while (teamFlags.transform.position != dest)
        {
            teamFlags.transform.position = Vector3.MoveTowards(teamFlags.transform.position, dest, Time.deltaTime * 1.0f);
            yield return null;
        }
        teamFlags.transform.position = dest;
        yield return null;
    }
}