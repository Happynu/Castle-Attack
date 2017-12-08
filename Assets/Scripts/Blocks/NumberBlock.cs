using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : Interactable
{
    public Text text;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateText();
	}

    public override void Interact()
    {
        Debug.Log("Number block hit");
    }

    void UpdateText()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        text.transform.position = screenPos;
    }
}
