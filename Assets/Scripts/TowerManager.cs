using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    List<GameObject> bricks = new List<GameObject>();
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

    // Use this for initialization
    void Start()
    {
        cameraDestination = GameObject.Find("CameraDestination").transform;
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            bricks.Add(brick);
            brick.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Brick>() != null)
                {
                    hit.collider.gameObject.GetComponent<Brick>().IncreaseDamageType();
                    foreach (Collider brickCollider in Physics.OverlapSphere(hit.transform.position, 10))
                    {
                        if (brickCollider.gameObject.GetComponent<Brick>() != null && hit.collider.gameObject != brickCollider.gameObject)
                        {
                            brickCollider.gameObject.GetComponent<Brick>().weakened = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            cameraMoving = true;
            foreach (GameObject brick in bricks)
            {
                if (brick != null)
                {
                    brick.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            foreach (GameObject interiorItem in towerInterior)
            {
                interiorItem.GetComponent<Rigidbody>().isKinematic = false;
            }
            Collapse();
        }

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
        Collider[] colliders = Physics.OverlapSphere(GameObject.Find("ExplosionPosition").transform.position, 20f);
        foreach (Collider collider in colliders)
        {
            Rigidbody body;
            if (collider.GetComponent<Rigidbody>() != null)
            {
                body = collider.GetComponent<Rigidbody>();
            }
            else
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
