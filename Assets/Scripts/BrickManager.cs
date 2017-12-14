using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

    public GameObject minusBrickPrefab;
    public GameObject plusBrickPrefab;
    public GameObject multiplierBrickPrefab;
    public GameObject numberBrickPrefab;
    public Material multiplierColor;
    public Material teamRedColor;
    public Material teamBlueColor;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool Addbrick(BrickType type, Vector3 pos, string value, BrickColor color)
    {
        GameObject newBrick;
        switch (type)
        {
            case BrickType.MinusBrick:
                newBrick = Instantiate(minusBrickPrefab, pos, Quaternion.identity);
                break;

            case BrickType.PlusBrick:
                newBrick = Instantiate(plusBrickPrefab, pos, Quaternion.identity);
                break;

            case BrickType.MultiplierBrick:
                newBrick = Instantiate(multiplierBrickPrefab, pos, Quaternion.identity);
                break;

            case BrickType.NumberBrick:
                newBrick = Instantiate(numberBrickPrefab, pos, Quaternion.identity);
                break;

            default:
                Debug.Log("Unknown Brick Type");
                return false;
        }
        return ChangeType(newBrick, color);

    }

    public bool ChangeType(GameObject brick, BrickColor color)
    {
        switch(color)
        {
            case BrickColor.TeamRed:
                brick.GetComponent<Renderer>().material = teamRedColor;
                break;

            case BrickColor.TeamBlue:
                brick.GetComponent<Renderer>().material = teamBlueColor;
                break;

            case BrickColor.Multiplier:
                brick.GetComponent<Renderer>().material = multiplierColor;
                break;

            default:
                Debug.Log("Unknown Brick Color");
                return false;
        }
        return true;
    }
}

public enum BrickColor
{
    Multiplier,
    TeamRed,
    TeamBlue
}

public enum BrickType
{
    MinusBrick,
    PlusBrick,
    MultiplierBrick,
    NumberBrick
}
