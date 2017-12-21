using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	List<GameObject> bricks = new List<GameObject>();

	// Use this for initialization
	void Start () {
		foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick")) {
			bricks.Add (brick);
			brick.GetComponent<Rigidbody> ().isKinematic = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.GetComponent<Brick> () != null) {
					hit.collider.gameObject.GetComponent<Brick> ().IncreaseDamageType ();
					//if (hit.collider.gameObject.transform.parent.name == "NorthWall") {

					foreach (Collider brickCollider in Physics.OverlapSphere(hit.transform.position, 10)) {
						if (brickCollider.gameObject.GetComponent<Brick> () != null) {
							brickCollider.gameObject.GetComponent<Brick> ().weakened = true;
							hit.collider.gameObject.GetComponent<Brick> ().IncreaseDamageType ();
							//Destroy(brickCollider.gameObject);
						}
					}

					//}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			foreach (GameObject brick in bricks) {
				if (brick.GetComponent<Brick>().weakened == true) {
					brick.GetComponent<Rigidbody> ().isKinematic = false;
				}
				brick.GetComponent<Rigidbody> ().AddForce (transform.forward * 500);
			}
		}
	}
}
