using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : Interactable
{

    void Start()
    {
    }

    public override void Interact()
    {
        Application.LoadLevel(1);
    }
}
