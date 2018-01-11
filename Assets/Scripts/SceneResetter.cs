using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : Interactable
{
    public override void Interact()
    {
        Debug.Log("Scene reload");
        SceneManager.LoadScene("Dev");
    }
}
