using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickChecker : MonoBehaviour {

    public LayerMask layer;
    public Camera cam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Click(Raycast(layer));
        }
    }

    //Translates the mouseclick to Unity coordinates.
    private Vector3 Translate()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //Spawns a raycast at the position for debugging.
    private RaycastHit Raycast(LayerMask layerMask)
    {
        RaycastHit hit;
        Vector3 position = Translate();
        Ray ray = new Ray(position, Vector3.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        return hit;
    }

    private void Click(RaycastHit hit)
    {
        if (hit.transform != null)
        {
            Interactable obj = hit.transform.GetComponent<Interactable>();

            if (obj != null)
            {
                obj.Interact();
            }
        }
    }
}
