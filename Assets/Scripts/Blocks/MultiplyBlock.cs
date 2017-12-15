using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyBlock : Interactable
{
    public Text text;

    void Start ()
    {
        UpdateText();
	}

    public override void Interact()
    {
        if (!timedout)
        {
            Debug.Log("Multiply block hit");
            GameManager.instance.HitBrick(this);
            StartCoroutine(HitTimout());
        }
    }

    void UpdateText()
    {
        text.text = "x";
    }

    
}
