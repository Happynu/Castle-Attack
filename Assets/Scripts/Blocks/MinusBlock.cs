using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusBlock : Interactable
{
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void Interact()
    {
        Debug.Log("Minus block hit");
    }
}
