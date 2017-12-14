using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text sumRed;
    public Text sumBlue;
    public Text finalValueRed;
    public Text finalValueBlue;
    public Text currentTeam;

	// Use this for initialization
	void Start ()
    {
        sumRed.text = "Sum: ";
        sumBlue.text = "Sum: ";
        finalValueBlue.text = "0";
        finalValueRed.text = "0";
        currentTeam.text = "No Team";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetFinalValue(int value, Team team)
    {
        switch(team.color)
        {
            case "Blue":
                finalValueBlue.text = value.ToString();
                break;

            case "Red":
                finalValueRed.text = value.ToString();
                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }
    }

    public void SetCurrentTeam(Team team)
    {
        switch (team.color)
        {
            case "Blue":
                currentTeam.text = team.color;
                break;

            case "Red":
                currentTeam.text = team.color;
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
            case "Blue":
                if (reset) { sumBlue.text = ""; }
                sumBlue.text += value + " ";
                break;

            case "Red":
                if (reset) { sumRed.text = ""; }
                sumRed.text += value + " ";
                break;

            default:
                Debug.Log("Teamcolor is unknown");
                break;
        }
    }
}
