using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusBlock : Interactable
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
        Debug.Log("Plus block hit");
        Destroy(this.gameObject);

    }

    void UpdateText()
    {
        text.text = "+";
    }
}
