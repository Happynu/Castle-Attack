using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyBlock : Interactable
{
    public Text text;

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
        Debug.Log("Multiply block hit");
        Destroy(this.gameObject);
    }

    void UpdateText()
    {
        text.text = "x";
    }
}
