using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : Interactable
{
    public Text text;
    public int number;

    void Start()
    {
        UpdateText();
    }

    public override void Interact()
    {
        if (GameManager.instance.timedout == false)
        {
            Debug.Log("Number block hit");
            GameManager.instance.HitBrick(this);
           
        }
    }

    public void UpdateText()
    {
        text.text = number.ToString();
    }
}
