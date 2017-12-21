using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text sumRed;
    public Text sumBlue;
    public Text finalValue;
    public Image BlueteamBanner;
    public Image RedteamBanner;

    // Use this for initialization 
    void Start()
    {
        sumRed.text = "Sum: ";
        sumBlue.text = "Sum: ";
        finalValue.text = "0";
    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void SetCurrentTeam(Team team)
    {
        switch (team.color)
        {
            case "blue":

                break;

            case "red":

                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }
    }

    public void UpdateSum(string value, bool reset, Team team)
    {
        switch (team.color)
        {
            case "blue":
                if (reset) { sumBlue.text = ""; }
                sumBlue.text += value + " ";
                break;

            case "red":
                if (reset) { sumRed.text = ""; }
                sumRed.text += value + " ";
                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }
    }

    public void RemoveBrick(Interactable brick, Team team)
    {
        switch (team.color)
        {
            case "blue":
                StartCoroutine(MoveBrick(brick, BlueteamBanner.transform.position));
                break;

            case "red":
                StartCoroutine(MoveBrick(brick, RedteamBanner.transform.position));
                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }
        
    }

    private IEnumerator MoveBrick(Interactable brick, Vector3 dest)
    {
        while(brick.transform.position != dest)
        {
            brick.transform.position = Vector3.MoveTowards(brick.transform.position, dest, 0.0025f);
            brick.transform.localScale *= 0.95f;
        }
        Destroy(brick.gameObject);
        yield return null;
    }
}