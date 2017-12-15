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
        if (!timedout)
        {
            Debug.Log("Multiply block hit");
            GameManager.instance.HitBrick(this);
            Destroy(this.gameObject);
            StartCoroutine(HitTimout());
        }
    }

    void UpdateText()
    {
        text.text = number.ToString();
    }
}
