using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusBlock : Interactable
{
    public Text text;

    void Start ()
    {
        UpdateText();
    }

    public override void Interact()
    {
        if (GameManager.instance.timedout == false)
        {
            StartCoroutine(HitTimout());
            Debug.Log("Multiply block hit");
            GameManager.instance.HitBrick(this);
        }
    }

    public void UpdateText()
    {
        text.text = "-";
    }


}
