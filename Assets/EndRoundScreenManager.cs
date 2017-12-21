using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundScreenManager : MonoBehaviour {
    private int scoreRed;
    private int scoreBlue;

    public GameObject red1;
    public GameObject red2;
    public GameObject red3;

    public GameObject blue1;
    public GameObject blue2;
    public GameObject blue3;

    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartEndRoundScreen(int score_red, int score_blue)
    {
        this.scoreRed = score_red;
        this.scoreBlue = score_blue;
        StartCoroutine(StartUp());
    }

    IEnumerator StartUp()
    {
        red1.gameObject.SetActive(false);
        red2.gameObject.SetActive(false);
        red3.gameObject.SetActive(false);

        blue1.gameObject.SetActive(false);
        blue2.gameObject.SetActive(false);
        blue3.gameObject.SetActive(false);

        Debug.Log("endroundscreen");
        switch (scoreRed)
        {
            case 1:
                Debug.Log("red score 1");
                red1.SetActive(true);
                break;
            case 2:
                red1.SetActive(true);
                red2.SetActive(true);
                break;
            case 3:
                red1.SetActive(true);
                red2.SetActive(true);
                red3.SetActive(true);
                break;
        }

        switch (scoreBlue)
        {
            case 1:
                Debug.Log("blue score 1");
                blue1.SetActive(true);
                break;
            case 2:
                blue1.SetActive(true);
                blue2.SetActive(true);
                break;
            case 3:
                blue1.SetActive(true);
                blue2.SetActive(true);
                blue3.SetActive(true);
                break;
        }

        yield return new WaitForSeconds(4f);

        this.gameObject.SetActive(false);
    }
}
