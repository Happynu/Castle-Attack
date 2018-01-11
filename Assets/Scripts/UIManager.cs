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

    // Use this for initialization 
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void StartUI()
    {
        number1Blue.text = "";
        number2Blue.text = "";
        operationBlue.text = "";
        resultBlue.text = "";

        number1Red.text = "";
        number2Red.text = "";
        operationRed.text = "";
        resultRed.text = "";
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

        float speed = 25f;

        float startDistance = Vector3.Distance(canvas.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvas.transform.position != dest)
        {
            //Move
            canvas.transform.position = Vector3.MoveTowards(canvas.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvas.transform.position, dest);
            if (currentDistance > (startDistance / 1.75))
            {
                canvas.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 1.01f;
            }
            else
            {
                canvas.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 1.01f;
            }

            Debug.Log("in while");
            yield return new WaitForFixedUpdate();
        }

        //Destroy label
        Debug.Log("destroying");
        Destroy(canvas.gameObject);

        //Next turn
        GameManager.instance.SwitchTeam();
        GameManager.instance.SpawnNewNumberBrick(new Vector2(brick.transform.position.x, brick.transform.position.y), (brick as NumberBlock).number);
        Destroy(brick.gameObject);
    }

    private IEnumerator CalculateAnimation()
    {
        //make sum bigger and smaller (pulse), while fading in the equals symbol
        yield return null;
    }

    private IEnumerator MoveOperationBrick(Interactable brick, Vector3 dest)
    {
        //detach label from brick
        GameObject goBrick = brick.transform.Find("Canvas").gameObject;
        Transform canvasCopy = Instantiate(goBrick, goBrick.transform).transform;

        float speed = 25f;

        float startDistance = Vector3.Distance(canvasCopy.transform.position, dest);
        float currentDistance = startDistance;

        //Move label to UI
        while (canvasCopy.transform.position != dest)
        {
            //Move
            canvasCopy.transform.position = Vector3.MoveTowards(canvasCopy.transform.position, dest, speed * Time.deltaTime);

            //Scale
            currentDistance = Vector3.Distance(canvasCopy.transform.position, dest);
            if (currentDistance > (startDistance / 1.75))
            {
                canvasCopy.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 1.01f;
            }
            else
            {
                canvasCopy.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 1.01f;
            }

            Debug.Log("in while");
            yield return new WaitForFixedUpdate();
        }

        //Destroy label
        Debug.Log("destroying");
        Destroy(canvasCopy.gameObject);

        //Next turn
        GameManager.instance.SwitchTeam();
    }

    string ConvertMultiplier(Multiplier m)
    {
        switch(m)
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
}