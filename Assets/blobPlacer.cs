using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobPlacer : MonoBehaviour {

    public GameObject blobberPrefap;
    public Camera cam;

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

    private void SpawnCube(Blob _blob)
    {
        GameObject.Instantiate(blobberPrefap, Translate(_blob.XPosition, _blob.YPosition), Quaternion.identity);
    }

    private Vector3 Translate(float x, float y)
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector3(((cam.transform.position.x) + (x * width) - (width/2)),
            ((cam.transform.position.y) + (y * height) - (height / 2)));
    }
}
