using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

    public Texture blue;
    public Texture red;

	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWinner(Team winner)
    {
        if(winner.color == "red")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.mainTexture = red;
        }
        else if(winner.color == "blue")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.mainTexture = blue;
        }
    }
}
