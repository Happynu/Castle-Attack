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
            GameManager.instance.StartHitTimout();
            Debug.Log("Number block hit");
            if (GameManager.instance.HitBrick(this))
            {
                GameManager.instance.SpawnNewNumberBrick(new Vector2(this.transform.position.x, this.transform.position.y), number);
                Destroy(this.gameObject);
            }
        }
    }

    public void UpdateText()
    {
        text.text = number.ToString();
    }
}
