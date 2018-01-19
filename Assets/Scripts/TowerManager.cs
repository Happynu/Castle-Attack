using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    List<GameObject> bricks = new List<GameObject>();
    public GameObject reset;
    private Transform cameraDestination;
    private bool cameraMoving;
    [SerializeField]
    private float cameraStep;
    [SerializeField]
    private float collapseForceMin;
    [SerializeField]
    private float collapseForceMax;
    [SerializeField]
    private List<GameObject> towerInterior;

    void Start()
    {
        cameraDestination = GameObject.Find("CameraDestination").transform;
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            bricks.Add(brick);
            brick.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Update()
    {
        if (cameraMoving)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraDestination.position, cameraStep);
            if (Camera.main.transform.position == cameraDestination.position)
            {
                cameraMoving = false;
            }
        }
    }

    public void Collapse()
    {
        cameraMoving = true;
        foreach (GameObject brick in bricks)
        {
            if (brick != null)
            {
                brick.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        foreach (GameObject towerFloor in towerInterior)
        {
            if (towerFloor.GetComponent<Rigidbody>() != null)
            {
                towerFloor.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(GameObject.Find("ExplosionPosition").transform.position, 20f);
        foreach (Collider collider in colliders)
        {
            Rigidbody body = null;
            if (collider.GetComponent<Rigidbody>() != null)
            {
                body = collider.GetComponent<Rigidbody>();
            }
            else if (collider.transform.parent != null && collider.transform.parent.GetComponent<Rigidbody>() != null)
            {
                body = collider.transform.parent.GetComponent<Rigidbody>();
            }

            if (body != null)
            {
                body.AddExplosionForce(Random.Range(collapseForceMin, collapseForceMax), GameObject.Find("ExplosionPosition").transform.position, 50);
            }
        }
    }
}
