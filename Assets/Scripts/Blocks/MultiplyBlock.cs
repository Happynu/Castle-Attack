using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyBlock : Interactable
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
        Debug.Log("Multiply block hit");
    }
}
