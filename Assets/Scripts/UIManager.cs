using System;
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

    public Animator animRed;
    public Animator animBlue;
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
                if (Blue.number1 != Int32.MinValue)
                {
                    number1Blue.text = Blue.number1.ToString();
                }
                else
                {
                    number1Blue.text = "";
                }

                operationBlue.text = ConvertMultiplier(Blue.operation);

                if (Blue.number2 != Int32.MinValue)
                {
                    number2Blue.text = Blue.number2.ToString();
                }
                else
                {
                    number2Blue.text = "";
                }

                if (Blue.result != Int32.MinValue)
                {
                    resultBlue.text = Blue.result.ToString();
                }
                else
                {
                    resultBlue.text = "";
                }
                break;

            case "red":
                if (Red.number1 != Int32.MinValue)
                {
                    number1Red.text = Red.number1.ToString();
                }
                else
                {
                    number1Red.text = "";
                }

                operationRed.text = ConvertMultiplier(Red.operation);

                if (Red.number2 != Int32.MinValue)
                {
                    number2Red.text = Red.number2.ToString();
                }
                else
                {
                    number2Red.text = "";
                }

                if (Red.result != Int32.MinValue)
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
        StartCoroutine(MoveNumberBrick(brick, team));
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

    private IEnumerator MoveNumberBrick(Interactable brick, Team team)
    {
        Vector3 dest = LabelPosition(team);
        //detach label from brick
        Transform canvas = brick.transform.Find("Canvas");
        Transform canvasCopy = Instantiate(canvas, canvas.transform).transform;
        canvas.gameObject.GetComponentInChildren<Text>().text = "";

        float speed = 3f;

        float startDistance = Vector3.Distance(canvasCopy.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvasCopy.transform.position.x != dest.x && canvasCopy.transform.position.y != dest.y)
        {
            //Move
            canvasCopy.transform.position = Vector3.MoveTowards(canvasCopy.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvasCopy.transform.transform.position, dest);
            if (currentDistance > (startDistance / 2))
            {
                canvasCopy.transform.localScale += Vector3.one * Time.deltaTime * 1.01f;
            }
            else
            {
                canvasCopy.transform.localScale -= Vector3.one * Time.deltaTime * 2.75f;
            }

            yield return null;
        }

        //Reset label
        Destroy(canvasCopy.gameObject);

        //Next turn
        GameManager.instance.SpawnNewNumberBrick(new Vector2(brick.transform.position.x, brick.transform.position.y), (brick as NumberBlock).number);

        UpdateUI(team);

        if (team.process == 4)
        {
            CalculateAnimation(team);
        }
        else
        {
            GameManager.instance.SwitchTeam();
        }
    }

    private IEnumerator MoveOperationBrick(Interactable brick, Vector3 dest)
    {
        Transform canvas = brick.transform.Find("Canvas");
        Transform canvasCopy = Instantiate(canvas, canvas.transform).transform;

        float speed = 3f;

        float startDistance = Vector3.Distance(canvasCopy.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvasCopy.transform.position.x != dest.x && canvasCopy.transform.position.y != dest.y)
        {
            //Move
            canvasCopy.transform.position = Vector3.MoveTowards(canvasCopy.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvasCopy.transform.position, dest);
            if (currentDistance > (startDistance / 2))
            {
                canvasCopy.transform.localScale += Vector3.one * Time.deltaTime * 1.01f;
            }
            else
            {
                canvasCopy.transform.localScale -= Vector3.one * Time.deltaTime * 2.75f;
            }

            yield return null;
        }

        //Destroy label copy
        Destroy(canvasCopy.gameObject);
        GameManager.instance.SwitchTeam();
    }

    public void CalculateAnimation(Team team)
    {
        StartCoroutine(Calculateanimation(team));
    }

    private IEnumerator Calculateanimation(Team team)
    {
        GameManager.instance.timedout = true;

        //Get teamlabels
        List<Text> sum = new List<Text>();
        Text number1;
        Text operation;
        Text number2;
        Text eq;
        Text result;

        Animator anim;

        if (team.color == "blue")
        {
            number1 = number1Blue;
            operation = operationBlue;
            number2 = number2Blue;
            eq = equalsBlue;
            result = resultBlue;

            anim = animBlue;
        }
        else //if (team.color == "red")
        {
            number1 = number1Red;
            operation = operationRed;
            number2 = number2Red;
            eq = equalsRed;
            result = resultRed;

            anim = animRed;
        }

        sum.Add(number1);
        sum.Add(operation);
        sum.Add(number2);
        sum.Add(eq);

        //make sum bigger and smaller (pulse), while fading in the equals symbol
        eq.color = Color.clear;

        anim.SetTrigger("Pulsate");

        while (eq.color.a < 1)
        {
            eq.color = new Color(1, 1, 1, eq.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.75f);

        team.Calculate();
        UpdateUI(team);

        anim.SetTrigger("ResultFade");
        //Pulse with equals symbol while solution fades in fast
        result.color = Color.clear;

        while (result.color.a < 1)
        {
            result.color = new Color(1, 1, 1, eq.color.a + 0.1f);
            Debug.Log("result fading in");
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(3);

        StartCoroutine(MoveResult(sum, result, team));
    }

    IEnumerator GetGoal(Text result)
    {
        yield return null;
    }

    IEnumerator MoveResult(List<Text> sum, Text result, Team team)
    {
        //Fade out sum and equals
        while (sum[0].color.a > 0)
        {
            foreach (Text label in sum)
            {
                label.color = new Color(1, 1, 1, sum[3].color.a - 0.2f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        //move solution to first slot
        Transform resultClone = Instantiate(result, result.transform.parent).transform;
        //resultClone.transform.parent = result.transform.parent;
        //resultClone.transform.localScale = new Vector3(1, 1, 1);

        result.text = "";

        while (resultClone.transform.position != sum[0].transform.position)
        {
            Debug.Log(resultClone.transform.position.x);
            resultClone.transform.position = Vector3.MoveTowards(resultClone.transform.position, sum[0].transform.position, 3f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        //reset variables
        Destroy(resultClone.gameObject);
        foreach (Text label in sum)
        {
            label.color = new Color(1, 1, 1, 1);
        }

        team.resetEquation();

        GameManager.instance.timedout = false;
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

    public void DestroyFlags()
    {
        StartCoroutine(_DestroyFlags());
    }

    public IEnumerator _DestroyFlags()
    {
        GameObject blueFlag = teamFlags.transform.Find("Flag-Blue").gameObject;
        GameObject redFlag = teamFlags.transform.Find("Flag-Red").gameObject;
        Vector3 dest = teamFlags.transform.position;
        dest.x = 0;

        while (blueFlag.transform.position != dest && redFlag.transform.position != dest)
        {
            blueFlag.transform.position = Vector3.MoveTowards(blueFlag.transform.position, dest, Time.deltaTime * 2.0f);
            redFlag.transform.position = Vector3.MoveTowards(redFlag.transform.position, dest, Time.deltaTime * 2.0f);
            yield return null;
        }

        Destroy(teamFlags);
        yield return null;
    }

    public void SwitchTeamFlags(Team currentTeam)
    {
        StartCoroutine(_SwitchTeamFlags(currentTeam));
    }

    public IEnumerator _SwitchTeamFlags(Team currentTeam)
    {
        if (teamFlags != null)
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

            while (teamFlags.transform.position != dest)
            {
                teamFlags.transform.position = Vector3.MoveTowards(teamFlags.transform.position, dest, Time.deltaTime * 1.0f);
                yield return null;
            }

            teamFlags.transform.position = dest;
            yield return null;
        }
    }
}