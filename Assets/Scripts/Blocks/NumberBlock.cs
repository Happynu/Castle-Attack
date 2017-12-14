using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : Interactable
{
    public Text text;
    public int number;

    // Use this for initialization
    void Start ()
    {
        UpdateText();
    }
	
	// Update is called once per frame
	void Update ()
    {
  
	}

    public override void Interact()
    {
        Debug.Log(number + " block hit");
        Destroy(this.gameObject);

    }

    void UpdateText()
    {
        text.text = number.ToString();
    }
}
