using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusBlock : Interactable
{
    public Text text;
    
    void Start ()
    {
        UpdateText();
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
