﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBlock : Interactable
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
        Debug.Log("Plus block hit");
    }
}
