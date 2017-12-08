using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobPlacer : MonoBehaviour {

    public GameObject blobberPrefap;
    public Camera cam;
    public LayerMask objectLayer;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        foreach (Blob blob in BlobTracking.Blobs)
        {
            if (OkayCheck(blob))
            {
                SpawnCube(blob);
                Click(Raycast(objectLayer, blob));
            }
        }
	}

    private bool OkayCheck(Blob _blob)
    {
        return (_blob.XPosition >= 0 && 
            _blob.XPosition <= 1 && 
            _blob.YPosition >= 0 && 
            _blob.YPosition <= 1 && 
            _blob.Height > 0 && 
            _blob.Width > 0);
    }

    // Old code used to spawn a cube where an object hit the screen.
    private void SpawnCube(Blob _blob)
    {
        GameObject blob = Instantiate(blobberPrefap, Translate(_blob.XPosition, _blob.YPosition), Quaternion.identity);
        Destroy(blob, 5);
    }

    //Translates the kinect coordinates to Unity coordinates.
    private Vector3 Translate(float x, float y)
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector3(((cam.transform.position.x) + (x * width) - (width/2)),
            ((cam.transform.position.y) + (y * height) - (height / 2)));
    }

    //Spawns a raycast at the position for debugging.
    private RaycastHit Raycast(LayerMask layerMask, Blob blob)
    {
        RaycastHit hit;
        Vector3 position = Translate(blob.XPosition, blob.YPosition);
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
