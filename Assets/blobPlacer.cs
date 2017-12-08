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
                Raycast(objectLayer, Translate(blob.XPosition, blob.YPosition));
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

    //Makes sure the object
    private Vector3 Translate(float x, float y)
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector3(((cam.transform.position.x) + (x * width) - (width/2)),
            ((cam.transform.position.y) + (y * height) - (height / 2)));
    }

    private RaycastHit Raycast(LayerMask layerMask, Vector3 position)
    {
        RaycastHit hit;
        Ray ray = new Ray(position, Vector3.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        if(hit.transform != null)
        {
            Debug.Log("Hit bOII");
        }
        return hit;
    }
}
