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
        if (GameManager.instance.timedout == false)
        {
            GameManager.instance.StartHitTimout();
            Debug.Log("Multiply block hit");
            if (GameManager.instance.HitBrick(this))
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void UpdateText()
    {
        text.text = number.ToString();
    }
}
