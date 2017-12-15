using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : Interactable
{
    public Text text;
    public int number;

    void Start ()
    {
        UpdateText();
    }

    public override void Interact()
    {
        Debug.Log(number + " block hit");
        bool succes = GameManager.instance.HitBrick(this);
        if (succes)
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateText()
    {
        text.text = number.ToString();
    }
}
