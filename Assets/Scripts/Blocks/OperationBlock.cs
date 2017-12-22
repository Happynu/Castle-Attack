using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationBlock : Interactable
{
    public Text text;
    public Multiplier multiplier;
    
    void Start ()
    {
        UpdateText();
	}

    public override void Interact()
    {
        if (GameManager.instance.timedout == false)
        {
            Debug.Log("Multiply block hit");
            GameManager.instance.HitBrick(this); 
        }
    }

    void UpdateText()
    {
        if (multiplier == Multiplier.PLUS)
        {
            text.text = "+";
        }
        else if (multiplier == Multiplier.MINUS)
        {
            text.text = "-";
        }
        else if (multiplier == Multiplier.MULTIPLY)
        {
            text.text = "x";
        }
        else
        {
            throw new System.NotImplementedException("Operation does not exist!");
        }
    }
}
